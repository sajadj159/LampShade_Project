#nullable enable

using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.ReadModel.Queries.Orders.SearchOrders;

public class SearchOrdersQuery : IRequest<List<OrderViewModel>>
{
    public long? AccountId { get; set; }
    public bool? IsCanceled { get; set; }
}
