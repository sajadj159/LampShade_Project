using MediatR;
using ShopManagement.Application.Contract.Order;

namespace LampShade.Api.Features.Orders.Commands.CancelOrder;

public class CancelOrderCommand : IRequest
{
    public long Id { get; set; }
}

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
{
    private readonly IOrderApplication _application;
    public CancelOrderCommandHandler(IOrderApplication application) => _application = application;

    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        _application.Cancel(request.Id);
        await Task.CompletedTask;
    }
}
