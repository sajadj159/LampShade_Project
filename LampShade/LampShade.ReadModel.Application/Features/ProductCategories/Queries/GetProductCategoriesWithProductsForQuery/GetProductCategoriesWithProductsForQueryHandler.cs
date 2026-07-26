using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LampShade.ReadModel.Contracts.ProductCategory;

using QueryRequest = LampShade.ReadModel.Contracts.Queries.ProductCategories.GetProductCategoriesWithProductsForQuery.GetProductCategoriesWithProductsForQuery;

namespace LampShade.ReadModel.Application.Features.ProductCategories.Queries.GetProductCategoriesWithProductsForQuery;



public class GetProductCategoriesWithProductsForQueryHandler : IRequestHandler<QueryRequest, List<ProductCategoryQueryModel>>
{
    private readonly IProductCategoryQuery _query;
    public GetProductCategoriesWithProductsForQueryHandler(IProductCategoryQuery query) => _query = query;
    public Task<List<ProductCategoryQueryModel>> Handle(QueryRequest r, CancellationToken c) => _query.GetProductCategoriesWithProductsAsync(c);
}
