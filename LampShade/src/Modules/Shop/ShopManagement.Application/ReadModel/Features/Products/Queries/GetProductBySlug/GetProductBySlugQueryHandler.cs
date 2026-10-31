using System.Threading;
using System.Threading.Tasks;
using ShopManagement.Application.ReadModel.Product;
using MediatR;

using ShopManagement.Application.ReadModel.Queries.Products.GetProductBySlug;

namespace ShopManagement.Application.ReadModel.Features.Products.Queries.GetProductBySlug;



public class GetProductBySlugQueryHandler : IRequestHandler<GetProductBySlugQuery, ProductQueryModel>
{
    private readonly IProductQuery _productQuery;

    public GetProductBySlugQueryHandler(IProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    public Task<ProductQueryModel> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        return _productQuery.GetProductDetailsAsync(request.Slug, cancellationToken);
    }
}
