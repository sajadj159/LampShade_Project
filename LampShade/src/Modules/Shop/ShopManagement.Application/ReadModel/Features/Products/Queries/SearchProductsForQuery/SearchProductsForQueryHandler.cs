using System.Threading;
using System.Threading.Tasks;
using ShopManagement.Application.ReadModel.Product;
using MediatR;

using QueryRequest = ShopManagement.Application.ReadModel.Queries.Products.SearchProductsForQuery.SearchProductsForQuery;

namespace ShopManagement.Application.ReadModel.Features.Products.Queries.SearchProductsForQuery;



public class SearchProductsForQueryHandler : IRequestHandler<QueryRequest, List<ProductQueryModel>>
{
    private readonly IProductQuery _productQuery;

    public SearchProductsForQueryHandler(IProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    public Task<List<ProductQueryModel>> Handle(QueryRequest request, CancellationToken cancellationToken)
    {
        return _productQuery.SearchAsync(request.Value, cancellationToken);
    }
}
