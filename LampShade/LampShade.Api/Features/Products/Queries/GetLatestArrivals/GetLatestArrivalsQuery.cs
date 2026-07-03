using _01_LampShadeQuery.Contract.Product;
using MediatR;

namespace LampShade.Api.Features.Products.Queries.GetLatestArrivals;

public class GetLatestArrivalsQuery : IRequest<List<ProductQueryModel>> { }

public class GetLatestArrivalsQueryHandler : IRequestHandler<GetLatestArrivalsQuery, List<ProductQueryModel>>
{
    private readonly IProductQuery _productQuery;

    public GetLatestArrivalsQueryHandler(IProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    public async Task<List<ProductQueryModel>> Handle(GetLatestArrivalsQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_productQuery.GetLatestArrivals());
    }
}
