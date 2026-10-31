using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.Products.EditProduct;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application.Features.Products.Commands.EditProduct;

public class EditProductCommandHandler(IProductRepository products, IFileUploader uploader, IHtmlSanitizer sanitizer)
    : IRequestHandler<EditProductCommand, OperationResult>
{
    public async Task<OperationResult> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var product = await products.GetProductWithCategoriesAsync(request.Id, cancellationToken);
        if (product is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (await products.ExistAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var slug = request.Slug.Slugify();
        var picturePath = $"{product.Category.Slug}/{slug}";
        var fileName = uploader.Upload(request.PictureUrl, picturePath);
        if (request.ClearMainPicture) product.ClearPicture();

        product.Edit(request.Name, request.Code, request.ShortDescription, sanitizer.Sanitize(request.Description), fileName,
            request.PictureTitle, request.PictureAlt, slug, request.Keywords, request.MetaDescription, request.CategoryId);
        ProductPictures.AddAdditional(product, request.AdditionalPictures, picturePath, uploader);
        return operation.Succeeded();
    }
}
