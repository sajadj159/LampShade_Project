using MediatR;
using ShopManagement.Application.Contract.Order;

namespace LampShade.Api.Features.Orders.Commands.PaymentSucceeded;

public class PaymentSucceededCommand : IRequest<string>
{
    public long OrderId { get; set; }
    public long RefId { get; set; }
}

public class PaymentSucceededCommandHandler : IRequestHandler<PaymentSucceededCommand, string>
{
    private readonly IOrderApplication _application;
    public PaymentSucceededCommandHandler(IOrderApplication application) => _application = application;

    public async Task<string> Handle(PaymentSucceededCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_application.PaymentSucceeded(request.OrderId, request.RefId));
    }
}
