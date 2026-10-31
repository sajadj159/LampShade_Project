using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Application.Contracts.Commands.Orders.CancelOrder;

namespace ShopManagement.Application.Features.Orders.Commands.CancelOrder;

public class CancelOrderCommandHandler(IOrderApplication application) : IRequestHandler<CancelOrderCommand>
{
    public Task Handle(CancelOrderCommand request, CancellationToken cancellationToken) => application.CancelAsync(request.Id, cancellationToken);
}
