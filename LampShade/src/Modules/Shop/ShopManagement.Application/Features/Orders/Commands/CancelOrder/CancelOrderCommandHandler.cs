using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.Orders.CancelOrder;
using ShopManagement.Application.Orders;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application.Features.Orders.Commands.CancelOrder;

public class CancelOrderCommandHandler(IOrderRepository orders, OrderAccessPolicy access) : IRequestHandler<CancelOrderCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult();
        var order = await orders.GetAsync(request.Id, cancellationToken);
        if (order is null || !access.CanAccess(order))
            return result.Failed(ApplicationMessages.RecordNotFound);
        if (order.IsPaid)
            return result.Failed("A paid order cannot be cancelled.");

        order.Cancel();
        return result.Succeeded();
    }
}
