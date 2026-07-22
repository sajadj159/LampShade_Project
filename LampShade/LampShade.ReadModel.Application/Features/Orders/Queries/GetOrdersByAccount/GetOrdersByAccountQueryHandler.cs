using MediatR;
using LampShade.ReadModel.Contracts.Order;

using LampShade.ReadModel.Contracts.Queries.Orders.GetOrdersByAccount;

namespace LampShade.ReadModel.Application.Features.Orders.Queries.GetOrdersByAccount;



public class GetOrdersByAccountQueryHandler : IRequestHandler<GetOrdersByAccountQuery, List<OrderQueryModel>>
{
    private readonly IOrderQuery _orderQuery;
    public GetOrdersByAccountQueryHandler(IOrderQuery orderQuery) => _orderQuery = orderQuery;

    public Task<List<OrderQueryModel>> Handle(GetOrdersByAccountQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_orderQuery.GetOrders(request.AccountId));
    }
}
