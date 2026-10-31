using System.Threading;
using System.Threading.Tasks;
#nullable enable

using MediatR;
using ShopManagement.Application.ReadModel.Admin;
using ShopManagement.Application.Contract.ProductCategory;

using ShopManagement.Application.ReadModel.Queries.ProductCategories.SearchProductCategories;

namespace ShopManagement.Application.ReadModel.Features.ProductCategories.Queries.SearchProductCategories;



public class SearchProductCategoriesQueryHandler : IRequestHandler<SearchProductCategoriesQuery, List<ProductCategoryViewModel>>
{
    private readonly IProductCategoryReadRepository _application;
    public SearchProductCategoriesQueryHandler(IProductCategoryReadRepository application) => _application = application;

    public Task<List<ProductCategoryViewModel>> Handle(SearchProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.Search(new ProductCategorySearchModel { Name = request.Name }));
    }
}
