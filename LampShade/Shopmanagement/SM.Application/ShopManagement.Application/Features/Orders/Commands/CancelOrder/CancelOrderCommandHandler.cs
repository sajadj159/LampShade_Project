using MediatR;
using ShopManagement.Application.Contract.Order;

using ShopManagement.Application.Contracts.Commands.Orders.CancelOrder;

namespace ShopManagement.Application.Features.Orders.Commands.CancelOrder;



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
