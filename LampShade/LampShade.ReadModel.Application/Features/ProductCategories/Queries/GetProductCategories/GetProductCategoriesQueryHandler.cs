using MediatR;
using ShopManagement.Application.Contract.ProductCategory;

using LampShade.ReadModel.Contracts.Queries.ProductCategories.GetProductCategories;

namespace LampShade.ReadModel.Application.Features.ProductCategories.Queries.GetProductCategories;



public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, List<ProductCategoryViewModel>>
{
    private readonly IProductCategoryApplication _application;
    public GetProductCategoriesQueryHandler(IProductCategoryApplication application) => _application = application;
    public Task<List<ProductCategoryViewModel>> Handle(GetProductCategoriesQuery r, CancellationToken c) => Task.FromResult(_application.GetProductCategories());
}
