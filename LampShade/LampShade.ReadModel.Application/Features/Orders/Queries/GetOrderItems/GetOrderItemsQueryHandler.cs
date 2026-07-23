using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.Contract.Order;

using LampShade.ReadModel.Contracts.Queries.Orders.GetOrderItems;

namespace LampShade.ReadModel.Application.Features.Orders.Queries.GetOrderItems;



public class GetOrderItemsQueryHandler : IRequestHandler<GetOrderItemsQuery, List<OrderItemViewModel>>
{
    private readonly IOrderApplication _application;
    public GetOrderItemsQueryHandler(IOrderApplication application) => _application = application;

    public Task<List<OrderItemViewModel>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.GetItemsBy(request.OrderId));
    }
}
