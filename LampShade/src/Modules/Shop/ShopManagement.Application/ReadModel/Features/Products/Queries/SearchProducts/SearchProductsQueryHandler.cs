using System.Threading;
using System.Threading.Tasks;
#nullable enable

using ShopManagement.Application.Contract.A.Product;
using MediatR;
using ShopManagement.Application.ReadModel.Admin;

using ShopManagement.Application.ReadModel.Queries.Products.SearchProducts;

namespace ShopManagement.Application.ReadModel.Features.Products.Queries.SearchProducts;



public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, List<ProductViewModel>>
{
    private readonly IProductReadRepository _application;

    public SearchProductsQueryHandler(IProductReadRepository application)
    {
        _application = application;
    }

    public Task<List<ProductViewModel>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new ProductSearchModel
        {
            Name = request.Name,
            Code = request.Code,
            CategoryId = request.CategoryId ?? 0
        };
        return Task.FromResult(_application.Search(searchModel));
    }
}
