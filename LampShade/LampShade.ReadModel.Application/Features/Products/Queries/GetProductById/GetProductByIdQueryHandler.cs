using System.Threading;
using System.Threading.Tasks;
using ShopManagement.Application.Contract.A.Product;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Products.GetProductById;

namespace LampShade.ReadModel.Application.Features.Products.Queries.GetProductById;



public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, EditProduct>
{
    private readonly IProductApplication _application;

    public GetProductByIdQueryHandler(IProductApplication application)
    {
        _application = application;
    }

    public Task<EditProduct> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.GetDetails(request.Id));
    }
}
