using System.Threading;
using System.Threading.Tasks;
using ShopManagement.Application.Contract.A.Product;
using MediatR;
using ShopManagement.Application.ReadModel.Admin;

using ShopManagement.Application.ReadModel.Queries.Products.GetProductById;

namespace ShopManagement.Application.ReadModel.Features.Products.Queries.GetProductById;



public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, EditProduct>
{
    private readonly IProductReadRepository _application;

    public GetProductByIdQueryHandler(IProductReadRepository application)
    {
        _application = application;
    }

    public Task<EditProduct> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.GetDetails(request.Id));
    }
}
