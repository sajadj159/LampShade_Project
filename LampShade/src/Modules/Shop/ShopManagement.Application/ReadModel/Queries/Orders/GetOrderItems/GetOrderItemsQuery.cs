using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.ReadModel.Queries.Orders.GetOrderItems;

public class GetOrderItemsQuery : IRequest<List<OrderItemViewModel>>
{
    public long OrderId { get; set; }
}
