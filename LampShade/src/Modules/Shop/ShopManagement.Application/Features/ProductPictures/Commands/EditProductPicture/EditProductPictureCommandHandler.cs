using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.ProductPictures.EditProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.Features.ProductPictures.Commands.EditProductPicture;

public class EditProductPictureCommandHandler(IProductPictureRepository pictures, IFileUploader uploader)
    : IRequestHandler<EditProductPictureCommand, OperationResult>
{
    public async Task<OperationResult> Handle(EditProductPictureCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var picture = await pictures.GetWithProductsAndCategoriesAsync(request.Id, cancellationToken);
        if (picture is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (await pictures.ExistAsync(x => x.PictureTitle == request.PictureTitle && x.ProductId == request.ProductId && x.Id != request.Id, cancellationToken))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var picturePath = uploader.Upload(request.PictureUrl, $"{picture.Product.Category.Slug}/{picture.Product.Slug}");
        picture.Edit(request.ProductId, picturePath, request.PictureTitle, request.PictureAlt);
        return operation.Succeeded();
    }
}
