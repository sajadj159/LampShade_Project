using System.Threading;
using System.Threading.Tasks;
using ShopManagement.Application.Contract.A.Product;
using MediatR;
using ShopManagement.Application.ReadModel.Admin;

using ShopManagement.Application.ReadModel.Queries.Products.GetProducts;

namespace ShopManagement.Application.ReadModel.Features.Products.Queries.GetProducts;



public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductViewModel>>
{
    private readonly IProductReadRepository _application;

    public GetProductsQueryHandler(IProductReadRepository application)
    {
        _application = application;
    }

    public Task<List<ProductViewModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.GetProducts());
    }
}
