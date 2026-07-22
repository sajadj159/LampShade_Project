using _01_LampShadeQuery.Contract.Product;
using MediatR;

namespace _01_LampShadeQuery.Features.Products.Queries.GetLatestArrivals;

public class GetLatestArrivalsQuery : IRequest<List<ProductQueryModel>> { }

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
