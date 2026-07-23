using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Application.Contracts.Commands.Orders.ApprovePaymentProof;

namespace ShopManagement.Application.Features.Orders.Commands.ApprovePaymentProof;

public class ApprovePaymentProofCommandHandler(IOrderApplication application) : IRequestHandler<ApprovePaymentProofCommand, string>
{
    public Task<string> Handle(ApprovePaymentProofCommand request, CancellationToken cancellationToken) => application.ApprovePaymentProofAsync(request.OrderId, cancellationToken);
}
