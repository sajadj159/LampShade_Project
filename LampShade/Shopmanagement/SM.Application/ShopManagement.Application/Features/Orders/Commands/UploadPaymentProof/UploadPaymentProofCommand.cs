using _0_Framework.Application;
using MediatR;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Features.Orders.Commands.UploadPaymentProof;

public class UploadPaymentProofCommand : IRequest<OperationResult>
{
    public long OrderId { get; set; }
    public IFormFile Proof { get; set; } = null!;
}

public class UploadPaymentProofCommandHandler : IRequestHandler<UploadPaymentProofCommand, OperationResult>
{
    private const long MaximumFileSize = 5 * 1024 * 1024;
    private readonly IFIleUploader _uploader;
    private readonly IOrderApplication _application;

    public UploadPaymentProofCommandHandler(IFIleUploader uploader, IOrderApplication application)
    {
        _uploader = uploader;
        _application = application;
    }

    public Task<OperationResult> Handle(UploadPaymentProofCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult();
        if (request.Proof == null || request.Proof.Length == 0 || !request.Proof.ContentType.StartsWith("image/") || request.Proof.Length > MaximumFileSize)
            return Task.FromResult(result.Failed("Upload an image smaller than 5 MB."));

        var fileKey = _uploader.Upload(request.Proof, "Orders/PaymentProofs");
        return Task.FromResult(_application.UploadPaymentProof(request.OrderId, fileKey));
    }

}
