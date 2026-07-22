using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;

namespace ShopManagement.Application.Features.ProductPictures.Queries.GetProductPictureById;

public class GetProductPictureByIdQuery : IRequest<EditProductPicture>
{
    public long Id { get; set; }
}

public class GetProductPictureByIdQueryHandler : IRequestHandler<GetProductPictureByIdQuery, EditProductPicture>
{
    private readonly IProductPictureApplication _application;

    public GetProductPictureByIdQueryHandler(IProductPictureApplication application)
    {
        _application = application;
    }

    public Task<EditProductPicture> Handle(GetProductPictureByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.GetDetails(request.Id));
    }
}
