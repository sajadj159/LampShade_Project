using ShopManagement.Application.Contract.A.Product;
using MediatR;

namespace ShopManagement.Application.Features.Products.Queries.GetProducts;

public class GetProductsQuery : IRequest<List<ProductViewModel>> { }

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductViewModel>>
{
    private readonly IProductApplication _application;

    public GetProductsQueryHandler(IProductApplication application)
    {
        _application = application;
    }

    public Task<List<ProductViewModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.GetProducts());
    }
}
