using ShopManagement.Application.Contract.A.Product;
using MediatR;

namespace ShopManagement.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<EditProduct>
{
    public long Id { get; set; }
}

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
