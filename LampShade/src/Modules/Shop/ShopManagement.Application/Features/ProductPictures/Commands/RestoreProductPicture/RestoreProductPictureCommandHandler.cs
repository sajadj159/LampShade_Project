using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.ProductPictures.RestoreProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.Features.ProductPictures.Commands.RestoreProductPicture;

public class RestoreProductPictureCommandHandler(IProductPictureRepository pictures) : IRequestHandler<RestoreProductPictureCommand, OperationResult>
{
    public async Task<OperationResult> Handle(RestoreProductPictureCommand request, CancellationToken cancellationToken)
    {
        var picture = await pictures.GetAsync(request.Id, cancellationToken);
        if (picture is null)
            return new OperationResult().Failed(ApplicationMessages.RecordNotFound);
        picture.Restore();
        return new OperationResult().Succeeded();
    }
}
