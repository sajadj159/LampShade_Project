using _01_LampShadeQuery.Contract.Product;
using MediatR;

namespace _01_LampShadeQuery.Features.Products.Queries.SearchProductsForQuery;

public class SearchProductsForQuery : IRequest<List<ProductQueryModel>>
{
    public string Value { get; set; } = string.Empty;
}

public class SearchProductsForQueryHandler : IRequestHandler<SearchProductsForQuery, List<ProductQueryModel>>
{
    private readonly IProductQuery _productQuery;

    public SearchProductsForQueryHandler(IProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    public Task<List<ProductQueryModel>> Handle(SearchProductsForQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_productQuery.Search(request.Value));
    }
}
