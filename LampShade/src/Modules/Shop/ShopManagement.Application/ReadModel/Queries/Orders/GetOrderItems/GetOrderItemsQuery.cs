using MediatR;
using ShopManagement.Application.Contract.Order;

namespace LampShade.ReadModel.Contracts.Queries.Orders.GetOrderItems;

public class GetOrderItemsQuery : IRequest<List<OrderItemViewModel>>
{
    public long OrderId { get; set; }
}
