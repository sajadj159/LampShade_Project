using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.ProductPictures.CreateProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.Features.ProductPictures.Commands.CreateProductPicture;

public class CreateProductPictureCommandHandler(IProductPictureRepository pictures, IProductRepository products, IFileUploader uploader)
    : IRequestHandler<CreateProductPictureCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateProductPictureCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (await pictures.ExistAsync(x => x.PictureTitle == request.PictureTitle && x.ProductId == request.ProductId, cancellationToken))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var product = await products.GetProductWithCategoriesAsync(request.ProductId, cancellationToken);
        if (product is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        var picturePath = uploader.Upload(request.PictureUrl, $"{product.Category.Slug}/{product.Slug}");
        pictures.Add(new Domain.ProductPictureAgg.ProductPicture(request.ProductId, picturePath, request.PictureTitle, request.PictureAlt));
        return operation.Succeeded();
    }
}
