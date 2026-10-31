using AccountManagement.Infrastructure.EFCore;
using AwesomeAssertions;
using InventoryManagement.Infrastructure.EFCore;
using LampShade.BuildingBlocks.Application;
using LampShade.BuildingBlocks.Infrastructure.Outbox;
using LampShade.IntegrationTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Application.Contracts.Commands.Orders.ApproveCashOnDelivery;
using ShopManagement.Application.Contracts.Commands.Orders.ApprovePaymentProof;
using ShopManagement.Application.Contracts.Commands.Orders.CancelOrder;
using ShopManagement.Application.Contracts.Commands.Orders.PlaceOrder;
using ShopManagement.Application.Contracts.Commands.Orders.UploadPaymentProof;
using ShopManagement.Application.ReadModel.Queries.Cart.ComputeCart;
using ShopManagement.Application.ReadModel.Queries.Orders.GetOrderItems;
using ShopManagement.Application.ReadModel.Queries.Orders.GetOrdersByAccount;
using ShopManagement.Infrastructure.EFCore;
using Xunit;
using static LampShade.IntegrationTests.Fixtures.ModularMonolithFixture;

namespace LampShade.IntegrationTests.Shop;

/// <summary>The checkout path end to end: Shop prices and stores the order, publishes OrderPaid through its outbox, Inventory and Account react.</summary>
[Collection(nameof(ModularMonolithCollection))]
public class OrderFlowTests(ModularMonolithFixture app)
{
    private const int Online = 1;
    private const int CashOnDelivery = 2;
    private CancellationToken CancellationToken => TestContext.Current.CancellationToken;

    [Fact]
    public async Task ComputeCart_UsesInventoryPriceAndDiscount_IgnoringClientPrices()
    {
        var product = await app.ProductAsync();
        await app.StockAsync(product.Id, 200_000, 5);
        await app.CustomerDiscountAsync(product.Id, 10);
        app.Auth.SignInAs(await CustomerAsync(), Roles.SystemUser);

        var cart = await app.SendAsync(new ComputeCartQuery { CartItems = [new CartItem { Id = product.Id, Count = 2, UnitPrice = 1, DiscountRate = 100 }] });

        cart.TotalAmount.Should().Be(400_000);
        cart.DiscountAmount.Should().Be(40_000);
        cart.PayAmount.Should().Be(360_000);
        cart.Items.Should().ContainSingle().Which.IsInStock.Should().BeTrue();
    }

    [Fact]
    public async Task PlaceOrder_IgnoresTamperedTotals_AndRejectsMissingStock()
    {
        var product = await app.ProductAsync();
        await app.StockAsync(product.Id, 50_000, 1);
        app.Auth.SignInAs(await CustomerAsync(), Roles.SystemUser);

        var placed = await app.SendAsync(new PlaceOrderCommand { PaymentMethod = Online, TotalAmount = 1, PayAmount = 1, Items = [new CartItem { Id = product.Id, Count = 1, UnitPrice = 1, DiscountRate = 99 }] });
        var tooMany = () => app.SendAsync(new PlaceOrderCommand { PaymentMethod = Online, Items = [new CartItem { Id = product.Id, Count = 3 }] });

        var order = await app.WithAsync<ShopContext, ShopManagement.Domain.OrderAgg.Order>(c => c.Orders.SingleAsync(x => x.Id == placed.OrderId, CancellationToken));
        order.PayAmount.Should().Be(50_000);
        order.Items.Should().ContainSingle().Which.DiscountRate.Should().Be(0);
        await tooMany.Should().ThrowAsync<AppValidationException>();
    }

    [Fact]
    public async Task ApprovedPaymentProof_PublishesOrderPaid_ThenInventoryReducesStockAndAccountTextsTheBuyer()
    {
        var product = await app.ProductAsync();
        var inventory = await app.StockAsync(product.Id, 100_000, 5);
        var customerId = await CustomerAsync(mobile: "09121112233");
        app.Auth.SignInAs(customerId, Roles.SystemUser);
        var placed = await app.SendAsync(new PlaceOrderCommand { PaymentMethod = Online, Items = [new CartItem { Id = product.Id, Count = 2 }] });
        var upload = await app.SendAsync(new UploadPaymentProofCommand { OrderId = placed.OrderId, Proof = Image("receipt.jpg") });

        app.Auth.SignInAs(1, Roles.Administrator);
        var trackingNumber = await app.SendAsync(new ApprovePaymentProofCommand { OrderId = placed.OrderId });

        // Nothing outside Shop changed yet: the event is still waiting in Shop's outbox.
        (await CurrentStockAsync(inventory.Id)).Should().Be(5);
        await app.ProcessOutboxAsync<ShopContext>();
        // Delivering the same event again must not reduce stock twice.
        await RedeliverLastShopEventAsync();

        upload.IsSucceeded.Should().BeTrue();
        trackingNumber.Should().NotBeNullOrWhiteSpace();
        (await CurrentStockAsync(inventory.Id)).Should().Be(3);
        app.Sms.Messages.Should().ContainSingle(x => x.Number == "09121112233" && x.Message.Contains(trackingNumber));
    }

    [Fact]
    public async Task ApproveCashOnDelivery_FailsWhenStockRanOut()
    {
        var product = await app.ProductAsync();
        var inventory = await app.StockAsync(product.Id, 100_000, 1);
        app.Auth.SignInAs(await CustomerAsync(), Roles.SystemUser);
        var placed = await app.SendAsync(new PlaceOrderCommand { PaymentMethod = CashOnDelivery, Items = [new CartItem { Id = product.Id, Count = 1 }] });
        await app.WithAsync<InventoryContext>(async c =>
        {
            var tracked = await c.Inventory.SingleAsync(x => x.Id == inventory.Id, CancellationToken);
            tracked.Reduce(1, 1, "Sold in store", 0);
            await c.SaveChangesAsync(CancellationToken);
        });

        app.Auth.SignInAs(1, Roles.Administrator);
        var trackingNumber = await app.SendAsync(new ApproveCashOnDeliveryCommand { OrderId = placed.OrderId });

        trackingNumber.Should().BeEmpty();
        (await app.WithAsync<ShopContext, bool>(c => c.Orders.Where(x => x.Id == placed.OrderId).Select(x => x.IsPaid).SingleAsync(CancellationToken))).Should().BeFalse();
    }

    [Fact]
    public async Task Customers_CannotReadOrCancelSomeoneElsesOrder()
    {
        var product = await app.ProductAsync();
        await app.StockAsync(product.Id, 10_000, 5);
        var owner = await CustomerAsync();
        var stranger = await CustomerAsync();
        app.Auth.SignInAs(owner, Roles.SystemUser);
        var placed = await app.SendAsync(new PlaceOrderCommand { PaymentMethod = Online, Items = [new CartItem { Id = product.Id, Count = 1 }] });

        app.Auth.SignInAs(stranger, Roles.SystemUser);
        var items = await app.SendAsync(new GetOrderItemsQuery { OrderId = placed.OrderId });
        var history = await app.SendAsync(new GetOrdersByAccountQuery { AccountId = owner });
        var cancel = await app.SendAsync(new CancelOrderCommand { Id = placed.OrderId });

        app.Auth.SignInAs(owner, Roles.SystemUser);
        var ownItems = await app.SendAsync(new GetOrderItemsQuery { OrderId = placed.OrderId });
        var ownCancel = await app.SendAsync(new CancelOrderCommand { Id = placed.OrderId });

        items.Should().BeEmpty();
        history.Should().NotContain(x => x.Id == placed.OrderId);
        cancel.IsSucceeded.Should().BeFalse();
        ownItems.Should().ContainSingle();
        ownCancel.IsSucceeded.Should().BeTrue();
        (await app.WithAsync<ShopContext, bool>(c => c.Orders.Where(x => x.Id == placed.OrderId).Select(x => x.IsCanceled).SingleAsync(CancellationToken))).Should().BeTrue();
    }

    private Task<long> CurrentStockAsync(long inventoryId) =>
        app.WithAsync<InventoryContext, long>(async c => (await c.Inventory.AsNoTracking().SingleAsync(x => x.Id == inventoryId, CancellationToken)).CalculateCurrentCount());

    private Task<long> CustomerAsync(string mobile = "09120000000") =>
        app.WithAsync<AccountContext, long>(async c =>
        {
            var account = new AccountManagement.Domain.AccountAgg.Account(Unique("customer"), "Test customer", "hash", mobile, 2, string.Empty, string.Empty, string.Empty);
            c.Accounts.Add(account);
            await c.SaveChangesAsync(CancellationToken);
            return account.Id;
        });

    private async Task RedeliverLastShopEventAsync()
    {
        var integrationEvent = await app.WithAsync<ShopContext, LampShade.BuildingBlocks.Application.IntegrationEvent>(async c =>
            (await c.Set<OutboxMessage>().OrderByDescending(x => x.OccurredOn).FirstAsync(CancellationToken)).ToIntegrationEvent());
        await app.WithAsync<MediatR.IPublisher>(publisher => publisher.Publish(integrationEvent, CancellationToken));
    }
}
