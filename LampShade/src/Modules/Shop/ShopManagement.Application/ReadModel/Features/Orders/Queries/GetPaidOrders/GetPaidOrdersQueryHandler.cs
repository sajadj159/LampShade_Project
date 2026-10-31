using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.ReadModel.Order;

using ShopManagement.Application.ReadModel.Queries.Orders.GetPaidOrders;

namespace ShopManagement.Application.ReadModel.Features.Orders.Queries.GetPaidOrders;



public class GetPaidOrdersQueryHandler : IRequestHandler<GetPaidOrdersQuery, List<OrderQueryModel>>
{
    private readonly IOrderQuery _orderQuery;
    public GetPaidOrdersQueryHandler(IOrderQuery orderQuery) => _orderQuery = orderQuery;

    public Task<List<OrderQueryModel>> Handle(GetPaidOrdersQuery request, CancellationToken cancellationToken)
    {
        return _orderQuery.GetPayedOrdersAsync(cancellationToken);
    }
}
