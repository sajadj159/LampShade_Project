using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.Product;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Products.GetProductBySlug;

namespace LampShade.ReadModel.Application.Features.Products.Queries.GetProductBySlug;



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
