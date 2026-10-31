using MediatR;
using ShopManagement.Application.ReadModel.Order;

namespace ShopManagement.Application.ReadModel.Queries.Orders.GetOrdersByAccount;

public class GetOrdersByAccountQuery : IRequest<List<OrderQueryModel>>
{
    public long AccountId { get; set; }
}
