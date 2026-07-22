#nullable enable

using ShopManagement.Application.Contract.A.Product;
using MediatR;

namespace ShopManagement.Application.Features.Products.Queries.SearchProducts;

public class SearchProductsQuery : IRequest<List<ProductViewModel>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long? CategoryId { get; set; }
}

public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, List<ProductViewModel>>
{
    private readonly IProductApplication _application;

    public SearchProductsQueryHandler(IProductApplication application)
    {
        _application = application;
    }

    public Task<List<ProductViewModel>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new ProductSearchModel
        {
            Name = request.Name,
            Code = request.Code,
            CategoryId = request.CategoryId ?? 0
        };
        return Task.FromResult(_application.Search(searchModel));
    }
}

