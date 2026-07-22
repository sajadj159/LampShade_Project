using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Contracts.Commands.Orders.ApprovePaymentProof;

public class ApprovePaymentProofCommand : IRequest<string>
{
    public long OrderId { get; set; }
}
