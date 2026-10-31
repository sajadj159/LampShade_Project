using System.Threading;
using System.Threading.Tasks;
using ShopManagement.Application.ReadModel.Product;
using MediatR;

using ShopManagement.Application.ReadModel.Queries.Products.GetLatestArrivals;

namespace ShopManagement.Application.ReadModel.Features.Products.Queries.GetLatestArrivals;



public class GetLatestArrivalsQueryHandler : IRequestHandler<GetLatestArrivalsQuery, List<ProductQueryModel>>
{
    private readonly IProductQuery _productQuery;

    public GetLatestArrivalsQueryHandler(IProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    public Task<List<ProductQueryModel>> Handle(GetLatestArrivalsQuery request, CancellationToken cancellationToken)
    {
        return _productQuery.GetLatestArrivalsAsync(cancellationToken);
    }
}
