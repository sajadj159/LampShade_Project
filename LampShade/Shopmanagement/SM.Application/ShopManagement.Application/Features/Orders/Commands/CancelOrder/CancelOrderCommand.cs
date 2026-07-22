using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Features.Orders.Commands.CancelOrder;

public class CancelOrderCommand : IRequest
{
    public long Id { get; set; }
}

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
{
    private readonly IOrderApplication _application;
    public CancelOrderCommandHandler(IOrderApplication application) => _application = application;

    public Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        _application.Cancel(request.Id);
        return Task.CompletedTask;
    }
}
