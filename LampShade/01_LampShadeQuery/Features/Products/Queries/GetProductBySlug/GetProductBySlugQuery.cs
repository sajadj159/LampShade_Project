using _01_LampShadeQuery.Contract.Product;
using MediatR;

namespace _01_LampShadeQuery.Features.Products.Queries.GetProductBySlug;

public class GetProductBySlugQuery : IRequest<ProductQueryModel>
{
    public string Slug { get; set; } = string.Empty;
}

public class GetProductBySlugQueryHandler : IRequestHandler<GetProductBySlugQuery, ProductQueryModel>
{
    private readonly IProductQuery _productQuery;

    public GetProductBySlugQueryHandler(IProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    public Task<ProductQueryModel> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_productQuery.GetProductDetails(request.Slug));
    }
}
