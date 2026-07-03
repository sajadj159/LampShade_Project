using MediatR;
using ShopManagement.Application.Contract.ProductCategory;

namespace LampShade.Api.Features.ProductCategories.Queries.SearchProductCategories;

public class SearchProductCategoriesQuery : IRequest<List<ProductCategoryViewModel>>
{
    public string? Name { get; set; }
}

public class SearchProductCategoriesQueryHandler : IRequestHandler<SearchProductCategoriesQuery, List<ProductCategoryViewModel>>
{
    private readonly IProductCategoryApplication _application;
    public SearchProductCategoriesQueryHandler(IProductCategoryApplication application) => _application = application;

    public async Task<List<ProductCategoryViewModel>> Handle(SearchProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_application.Search(new ProductCategorySearchModel { Name = request.Name }));
    }
}
