using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.ReadModel.ProductCategory;

using QueryRequest = ShopManagement.Application.ReadModel.Queries.ProductCategories.GetProductCategoriesWithProductsForQuery.GetProductCategoriesWithProductsForQuery;

namespace ShopManagement.Application.ReadModel.Features.ProductCategories.Queries.GetProductCategoriesWithProductsForQuery;



public class GetProductCategoriesWithProductsForQueryHandler : IRequestHandler<QueryRequest, List<ProductCategoryQueryModel>>
{
    private readonly IProductCategoryQuery _query;
    public GetProductCategoriesWithProductsForQueryHandler(IProductCategoryQuery query) => _query = query;
    public Task<List<ProductCategoryQueryModel>> Handle(QueryRequest r, CancellationToken c) => _query.GetProductCategoriesWithProductsAsync(c);
}
