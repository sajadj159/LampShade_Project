using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.ReadModel.ProductCategory;

using ShopManagement.Application.ReadModel.Queries.ProductCategories.GetProductCategoryWithProducts;

namespace ShopManagement.Application.ReadModel.Features.ProductCategories.Queries.GetProductCategoryWithProducts;



public class GetProductCategoryWithProductsQueryHandler : IRequestHandler<GetProductCategoryWithProductsQuery, ProductCategoryQueryModel>
{
    private readonly IProductCategoryQuery _query;
    public GetProductCategoryWithProductsQueryHandler(IProductCategoryQuery query) => _query = query;
    public Task<ProductCategoryQueryModel> Handle(GetProductCategoryWithProductsQuery r, CancellationToken c) => _query.GetProductCategoryWithProductsAsync(r.Slug, c);
}
