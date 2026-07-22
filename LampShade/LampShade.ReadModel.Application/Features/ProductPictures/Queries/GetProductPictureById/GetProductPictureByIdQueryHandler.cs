using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.ProductPictures.GetProductPictureById;

namespace LampShade.ReadModel.Application.Features.ProductPictures.Queries.GetProductPictureById;



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
