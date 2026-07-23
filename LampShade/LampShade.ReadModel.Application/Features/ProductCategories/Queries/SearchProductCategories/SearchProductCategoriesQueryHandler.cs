using System.Threading;
using System.Threading.Tasks;
#nullable enable

using MediatR;
using ShopManagement.Application.Contract.ProductCategory;

using LampShade.ReadModel.Contracts.Queries.ProductCategories.SearchProductCategories;

namespace LampShade.ReadModel.Application.Features.ProductCategories.Queries.SearchProductCategories;



public class SearchProductCategoriesQueryHandler : IRequestHandler<SearchProductCategoriesQuery, List<ProductCategoryViewModel>>
{
    private readonly IProductCategoryApplication _application;
    public SearchProductCategoriesQueryHandler(IProductCategoryApplication application) => _application = application;

    public Task<List<ProductCategoryViewModel>> Handle(SearchProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.Search(new ProductCategorySearchModel { Name = request.Name }));
    }
}
