using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.ProductPictures.RemoveProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.Features.ProductPictures.Commands.RemoveProductPicture;

public class RemoveProductPictureCommandHandler(IProductPictureRepository pictures) : IRequestHandler<RemoveProductPictureCommand, OperationResult>
{
    public async Task<OperationResult> Handle(RemoveProductPictureCommand request, CancellationToken cancellationToken)
    {
        var picture = await pictures.GetAsync(request.Id, cancellationToken);
        if (picture is null)
            return new OperationResult().Failed(ApplicationMessages.RecordNotFound);
        picture.Remove();
        return new OperationResult().Succeeded();
    }
}
