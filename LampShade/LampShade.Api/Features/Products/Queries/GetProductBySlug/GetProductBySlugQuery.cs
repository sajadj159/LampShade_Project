using _01_LampShadeQuery.Contract.Product;
using MediatR;

namespace LampShade.Api.Features.Products.Queries.GetProductBySlug;

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

    public async Task<ProductQueryModel> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_productQuery.GetProductDetails(request.Slug));
    }
}
