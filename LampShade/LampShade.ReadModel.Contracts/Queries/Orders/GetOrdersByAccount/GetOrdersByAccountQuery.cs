using MediatR;
using LampShade.ReadModel.Contracts.Order;

namespace LampShade.ReadModel.Contracts.Queries.Orders.GetOrdersByAccount;

public class GetOrdersByAccountQuery : IRequest<List<OrderQueryModel>>
{
    public long AccountId { get; set; }
}
