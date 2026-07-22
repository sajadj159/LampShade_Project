using MediatR;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Application.Features.ProductCategories.Queries.GetProductCategories;

public class GetProductCategoriesQuery : IRequest<List<ProductCategoryViewModel>> { }

public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, List<ProductCategoryViewModel>>
{
    private readonly IProductCategoryApplication _application;
    public GetProductCategoriesQueryHandler(IProductCategoryApplication application) => _application = application;
    public Task<List<ProductCategoryViewModel>> Handle(GetProductCategoriesQuery r, CancellationToken c) => Task.FromResult(_application.GetProductCategories());
}
