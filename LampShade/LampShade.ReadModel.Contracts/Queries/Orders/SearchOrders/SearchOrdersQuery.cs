#nullable enable

using MediatR;
using ShopManagement.Application.Contract.Order;

namespace LampShade.ReadModel.Contracts.Queries.Orders.SearchOrders;

public class SearchOrdersQuery : IRequest<List<OrderViewModel>>
{
    public long? AccountId { get; set; }
    public bool? IsCanceled { get; set; }
}
