using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LampShade.ReadModel.Contracts.Order;

using LampShade.ReadModel.Contracts.Queries.Orders.GetPaidOrders;

namespace LampShade.ReadModel.Application.Features.Orders.Queries.GetPaidOrders;



public class GetPaidOrdersQueryHandler : IRequestHandler<GetPaidOrdersQuery, List<OrderQueryModel>>
{
    private readonly IOrderQuery _orderQuery;
    public GetPaidOrdersQueryHandler(IOrderQuery orderQuery) => _orderQuery = orderQuery;

    public Task<List<OrderQueryModel>> Handle(GetPaidOrdersQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_orderQuery.GetPayedOrders());
    }
}
