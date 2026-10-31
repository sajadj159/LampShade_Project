using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.Orders.UploadPaymentProof;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application.Features.Orders.Commands.UploadPaymentProof;

public class UploadPaymentProofCommandHandler(IFileUploader uploader, IOrderRepository orders, IAuthHelper authHelper)
    : IRequestHandler<UploadPaymentProofCommand, OperationResult>
{
    private const int OnlinePayment = 1;

    public async Task<OperationResult> Handle(UploadPaymentProofCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult();
        if (request.Proof is null || request.Proof.Length == 0)
            return result.Failed("Upload an image smaller than 5 MB.");

        var order = await orders.GetAsync(request.OrderId, cancellationToken);
        if (order is null || order.AccountId != authHelper.CurrentAccountId() || order.PaymentMethod != OnlinePayment || order.IsCanceled || order.IsPaid)
            return result.Failed("Payment proof cannot be uploaded for this order.");

        order.SetPaymentProof(uploader.Upload(request.Proof, "Orders/PaymentProofs"));
        return result.Succeeded();
    }
}
