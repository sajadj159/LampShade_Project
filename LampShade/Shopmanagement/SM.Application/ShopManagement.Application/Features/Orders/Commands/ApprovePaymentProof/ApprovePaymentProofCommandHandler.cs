using MediatR;
using ShopManagement.Application.Contract.Order;

using ShopManagement.Application.Contracts.Commands.Orders.ApprovePaymentProof;

namespace ShopManagement.Application.Features.Orders.Commands.ApprovePaymentProof;



public class ApprovePaymentProofCommandHandler : IRequestHandler<ApprovePaymentProofCommand, string>
{
    private readonly IOrderApplication _application;
    public ApprovePaymentProofCommandHandler(IOrderApplication application) => _application = application;

    public Task<string> Handle(ApprovePaymentProofCommand request, CancellationToken cancellationToken)
        => Task.FromResult(_application.ApprovePaymentProof(request.OrderId));
}
