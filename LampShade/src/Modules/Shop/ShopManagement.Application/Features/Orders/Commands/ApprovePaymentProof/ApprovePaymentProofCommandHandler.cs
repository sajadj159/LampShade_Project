using MediatR;
using ShopManagement.Application.Contracts.Commands.Orders.ApprovePaymentProof;
using ShopManagement.Application.Orders;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application.Features.Orders.Commands.ApprovePaymentProof;

public class ApprovePaymentProofCommandHandler(IOrderRepository orders, OrderConfirmation confirmation) : IRequestHandler<ApprovePaymentProofCommand, string>
{
    private const int OnlinePayment = 1;

    public async Task<string> Handle(ApprovePaymentProofCommand request, CancellationToken cancellationToken)
    {
        var order = await orders.GetAsync(request.OrderId, cancellationToken);
        if (order is null || order.PaymentMethod != OnlinePayment || string.IsNullOrWhiteSpace(order.PaymentProofUrl) || order.IsCanceled)
            return string.Empty;
        return order.IsPaid ? order.IssueTrackingNumber : await confirmation.ConfirmAsync(order, cancellationToken);
    }
}
