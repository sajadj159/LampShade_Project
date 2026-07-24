using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.Product;
using MediatR;

using QueryRequest = LampShade.ReadModel.Contracts.Queries.Products.SearchProductsForQuery.SearchProductsForQuery;

namespace LampShade.ReadModel.Application.Features.Products.Queries.SearchProductsForQuery;



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
