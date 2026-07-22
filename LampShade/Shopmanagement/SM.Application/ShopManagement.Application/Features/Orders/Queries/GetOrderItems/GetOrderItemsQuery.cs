using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Features.Orders.Queries.GetOrderItems;

public class GetOrderItemsQuery : IRequest<List<OrderItemViewModel>>
{
    public long OrderId { get; set; }
}

public class GetOrderItemsQueryHandler : IRequestHandler<GetOrderItemsQuery, List<OrderItemViewModel>>
{
    private readonly IOrderApplication _application;
    public GetOrderItemsQueryHandler(IOrderApplication application) => _application = application;

    public Task<List<OrderItemViewModel>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.GetItemsBy(request.OrderId));
    }
}
