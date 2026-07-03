using _01_LampShadeQuery.Contract.Product;
using MediatR;

namespace LampShade.Api.Features.Products.Queries.SearchProductsForQuery;

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

    public async Task<List<ProductQueryModel>> Handle(SearchProductsForQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_productQuery.Search(request.Value));
    }
}
