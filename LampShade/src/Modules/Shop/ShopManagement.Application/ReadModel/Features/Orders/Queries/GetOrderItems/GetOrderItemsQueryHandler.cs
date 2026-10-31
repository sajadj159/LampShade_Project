using MediatR;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Application.Orders;
using ShopManagement.Application.ReadModel.Admin;
using ShopManagement.Application.ReadModel.Queries.Orders.GetOrderItems;

namespace ShopManagement.Application.ReadModel.Features.Orders.Queries.GetOrderItems;

public class GetOrderItemsQueryHandler(IOrderReadRepository orders, OrderAccessPolicy access) : IRequestHandler<GetOrderItemsQuery, List<OrderItemViewModel>>
{
    public async Task<List<OrderItemViewModel>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken) =>
        await access.CanAccessAsync(request.OrderId, cancellationToken) ? orders.GetItemsBy(request.OrderId) : [];
}
