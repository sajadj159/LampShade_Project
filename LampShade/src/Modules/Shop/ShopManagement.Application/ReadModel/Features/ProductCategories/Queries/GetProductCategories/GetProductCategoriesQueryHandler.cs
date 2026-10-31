using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.ReadModel.Admin;
using ShopManagement.Application.Contract.ProductCategory;

using ShopManagement.Application.ReadModel.Queries.ProductCategories.GetProductCategories;

namespace ShopManagement.Application.ReadModel.Features.ProductCategories.Queries.GetProductCategories;



public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, List<ProductCategoryViewModel>>
{
    private readonly IProductCategoryReadRepository _application;
    public GetProductCategoriesQueryHandler(IProductCategoryReadRepository application) => _application = application;
    public Task<List<ProductCategoryViewModel>> Handle(GetProductCategoriesQuery r, CancellationToken c) => Task.FromResult(_application.GetProductCategories());
}
