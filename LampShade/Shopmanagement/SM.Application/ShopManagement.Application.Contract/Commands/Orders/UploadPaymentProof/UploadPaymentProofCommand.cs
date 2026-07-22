using _0_Framework.Application;
using MediatR;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Contracts.Commands.Orders.UploadPaymentProof;

public class UploadPaymentProofCommand : IRequest<OperationResult>
{
    public long OrderId { get; set; }
    public IFormFile Proof { get; set; } = null!;
}
