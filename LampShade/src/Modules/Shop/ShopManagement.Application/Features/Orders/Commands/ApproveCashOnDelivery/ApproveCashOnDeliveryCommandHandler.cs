using MediatR;
using ShopManagement.Application.Contracts.Commands.Orders.ApproveCashOnDelivery;
using ShopManagement.Application.Orders;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application.Features.Orders.Commands.ApproveCashOnDelivery;

public class ApproveCashOnDeliveryCommandHandler(IOrderRepository orders, OrderConfirmation confirmation) : IRequestHandler<ApproveCashOnDeliveryCommand, string>
{
    private const int CashOnDelivery = 2;

    public async Task<string> Handle(ApproveCashOnDeliveryCommand request, CancellationToken cancellationToken)
    {
        var order = await orders.GetAsync(request.OrderId, cancellationToken);
        if (order is null || order.PaymentMethod != CashOnDelivery || order.IsCanceled)
            return string.Empty;
        return order.IsPaid ? order.IssueTrackingNumber : await confirmation.ConfirmAsync(order, cancellationToken);
    }
}
