using MediatR;
using ShopManagement.Application.Contract.Order;

using ShopManagement.Application.Contracts.Commands.Orders.PaymentSucceeded;

namespace ShopManagement.Application.Features.Orders.Commands.PaymentSucceeded;



public class PaymentSucceededCommandHandler : IRequestHandler<PaymentSucceededCommand, string>
{
    private readonly IOrderApplication _application;
    public PaymentSucceededCommandHandler(IOrderApplication application) => _application = application;

    public Task<string> Handle(PaymentSucceededCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.PaymentSucceeded(request.OrderId, request.RefId));
    }
}
