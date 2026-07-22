using MediatR;
using _01_LampShadeQuery.Contract.Order;

namespace _01_LampShadeQuery.Features.Orders.Queries.GetPaidOrders;

public class GetPaidOrdersQuery : IRequest<List<OrderQueryModel>> { }

public class GetPaidOrdersQueryHandler : IRequestHandler<GetPaidOrdersQuery, List<OrderQueryModel>>
{
    private readonly IOrderQuery _orderQuery;
    public GetPaidOrdersQueryHandler(IOrderQuery orderQuery) => _orderQuery = orderQuery;

    public Task<List<OrderQueryModel>> Handle(GetPaidOrdersQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_orderQuery.GetPayedOrders());
    }
}
