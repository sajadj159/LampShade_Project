using LampShade.ReadModel.Contracts.Product;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Products.GetLatestArrivals;

namespace LampShade.ReadModel.Application.Features.Products.Queries.GetLatestArrivals;



public class GetLatestArrivalsQueryHandler : IRequestHandler<GetLatestArrivalsQuery, List<ProductQueryModel>>
{
    private readonly IProductQuery _productQuery;

    public GetLatestArrivalsQueryHandler(IProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    public Task<List<ProductQueryModel>> Handle(GetLatestArrivalsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_productQuery.GetLatestArrivals());
    }
}
