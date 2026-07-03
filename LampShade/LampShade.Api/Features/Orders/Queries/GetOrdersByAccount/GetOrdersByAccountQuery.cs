using MediatR;
using _01_LampShadeQuery.Contract.Order;

namespace LampShade.Api.Features.Orders.Queries.GetOrdersByAccount;

public class GetOrdersByAccountQuery : IRequest<List<OrderQueryModel>>
{
    public long AccountId { get; set; }
}

public class GetOrdersByAccountQueryHandler : IRequestHandler<GetOrdersByAccountQuery, List<OrderQueryModel>>
{
    private readonly IOrderQuery _orderQuery;
    public GetOrdersByAccountQueryHandler(IOrderQuery orderQuery) => _orderQuery = orderQuery;

    public async Task<List<OrderQueryModel>> Handle(GetOrdersByAccountQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_orderQuery.GetOrders(request.AccountId));
    }
}
