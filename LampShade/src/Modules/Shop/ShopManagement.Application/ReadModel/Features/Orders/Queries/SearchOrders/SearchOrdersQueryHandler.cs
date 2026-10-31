using System.Threading;
using System.Threading.Tasks;
#nullable enable

using MediatR;
using ShopManagement.Application.ReadModel.Admin;
using ShopManagement.Application.Contract.Order;

using ShopManagement.Application.ReadModel.Queries.Orders.SearchOrders;

namespace ShopManagement.Application.ReadModel.Features.Orders.Queries.SearchOrders;



public class SearchOrdersQueryHandler : IRequestHandler<SearchOrdersQuery, List<OrderViewModel>>
{
    private readonly IOrderReadRepository _application;
    public SearchOrdersQueryHandler(IOrderReadRepository application) => _application = application;

    public Task<List<OrderViewModel>> Handle(SearchOrdersQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new OrderSearchModel { AccountId = request.AccountId ?? 0, IsCanceled = request.IsCanceled ?? false };
        return _application.SearchAsync(searchModel, cancellationToken);
    }
}
