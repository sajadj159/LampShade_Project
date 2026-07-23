using System.Threading;
using System.Threading.Tasks;
#nullable enable

using MediatR;
using ShopManagement.Application.Contract.Order;

using LampShade.ReadModel.Contracts.Queries.Orders.SearchOrders;

namespace LampShade.ReadModel.Application.Features.Orders.Queries.SearchOrders;



public class SearchOrdersQueryHandler : IRequestHandler<SearchOrdersQuery, List<OrderViewModel>>
{
    private readonly IOrderApplication _application;
    public SearchOrdersQueryHandler(IOrderApplication application) => _application = application;

    public Task<List<OrderViewModel>> Handle(SearchOrdersQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new OrderSearchModel { AccountId = request.AccountId ?? 0, IsCanceled = request.IsCanceled ?? false };
        return Task.FromResult(_application.Search(searchModel));
    }
}
