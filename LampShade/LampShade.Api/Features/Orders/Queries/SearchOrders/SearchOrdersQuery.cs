using MediatR;
using ShopManagement.Application.Contract.Order;

namespace LampShade.Api.Features.Orders.Queries.SearchOrders;

public class SearchOrdersQuery : IRequest<List<OrderViewModel>>
{
    public long? AccountId { get; set; }
    public bool? IsCanceled { get; set; }
}

public class SearchOrdersQueryHandler : IRequestHandler<SearchOrdersQuery, List<OrderViewModel>>
{
    private readonly IOrderApplication _application;
    public SearchOrdersQueryHandler(IOrderApplication application) => _application = application;

    public async Task<List<OrderViewModel>> Handle(SearchOrdersQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new OrderSearchModel { AccountId = request.AccountId ?? 0, IsCanceled = request.IsCanceled ?? false };
        return await Task.FromResult(_application.Search(searchModel));
    }
}

