using _0_Framework.Application;
using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Contracts.Commands.Orders.ApprovePaymentProof;

public class ApprovePaymentProofCommand : ICommand<string>
{
    public long OrderId { get; set; }
}
