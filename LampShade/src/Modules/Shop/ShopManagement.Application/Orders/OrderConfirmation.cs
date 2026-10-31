using InventoryManagement.Contracts;
using LampShade.BuildingBlocks.Application;
using ShopManagement.Contracts.IntegrationEvents;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application.Orders;

/// <summary>
/// Marks an order as paid and publishes <see cref="OrderPaidIntegrationEvent"/>. Stock is checked here
/// so an approval never promises goods Inventory cannot deliver; Inventory reduces it from the event.
/// </summary>
public sealed class OrderConfirmation(IInventoryModuleApi inventory, IShopOutbox outbox, IAuthHelper authHelper)
{
    public async Task<string> ConfirmAsync(Domain.OrderAgg.Order order, CancellationToken cancellationToken)
    {
        if (order.IsCanceled || order.IsPaid)
            return string.Empty;

        var stock = await inventory.GetStockAsync(order.Items.Select(x => x.ProductId), cancellationToken);
        var everythingAvailable = order.Items.All(item => stock.GetValueOrDefault(item.ProductId)?.CurrentCount >= item.Count);
        if (!everythingAvailable)
            return string.Empty;

        var issueTrackingNumber = CodeGenerator.Generate("S");
        order.PaymentSucceeded(0);
        order.SetIssueTrackingNumber(issueTrackingNumber);

        outbox.Add(new OrderPaidIntegrationEvent(
            order.Id,
            order.AccountId,
            issueTrackingNumber,
            authHelper.CurrentAccountId(),
            order.Items.Select(x => new OrderPaidItem(x.ProductId, x.Count)).ToList()));

        return issueTrackingNumber;
    }
}
