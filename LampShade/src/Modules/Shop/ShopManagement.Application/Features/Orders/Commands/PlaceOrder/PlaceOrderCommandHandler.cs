using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.Orders.PlaceOrder;
using ShopManagement.Application.Orders;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application.Features.Orders.Commands.PlaceOrder;

/// <summary>
/// Creates an order from the requested product ids and counts. Totals the client sends are ignored:
/// the cart is priced again on the server and every item must be in stock.
/// </summary>
public class PlaceOrderCommandHandler(IOrderRepository orders, CartCalculator calculator, IAuthHelper authHelper)
    : IRequestHandler<PlaceOrderCommand, PlaceOrderResult>
{
    public async Task<PlaceOrderResult> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        if (Contract.PaymentMethod.GetBy(request.PaymentMethod) is null)
            throw new AppValidationException("Unknown payment method.");

        var cart = await calculator.ComputeAsync(request.Items, cancellationToken);
        if (cart.Items.Count == 0)
            throw new AppValidationException("The cart is empty.");

        var unavailable = cart.Items.FirstOrDefault(x => !x.IsInStock);
        if (unavailable is not null)
            throw new AppValidationException($"'{unavailable.Name}' is not available in the requested quantity.");

        var order = new Domain.OrderAgg.Order(authHelper.CurrentAccountId(), cart.TotalAmount, request.PaymentMethod, cart.DiscountAmount, cart.PayAmount);
        foreach (var item in cart.Items)
            order.AddItem(new OrderItem(item.Id, item.Count, item.UnitPrice, item.DiscountRate));

        orders.Add(order);
        return new PlaceOrderResult(() => order.Id);
    }
}
