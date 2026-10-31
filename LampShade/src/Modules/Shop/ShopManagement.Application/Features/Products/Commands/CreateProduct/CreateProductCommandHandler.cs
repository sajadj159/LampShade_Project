using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.Products.CreateProduct;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository products, IProductCategoryRepository categories, IFileUploader uploader, IHtmlSanitizer sanitizer)
    : IRequestHandler<CreateProductCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (await products.ExistAsync(x => x.Name == request.Name, cancellationToken))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var slug = request.Slug.Slugify();
        var categorySlug = await categories.GetSlugByAsync(request.CategoryId, cancellationToken);
        var picturePath = $"{categorySlug}/{slug}";

        var product = new Domain.ProductAgg.Product(request.Name, request.Code, request.ShortDescription,
            sanitizer.Sanitize(request.Description), uploader.Upload(request.PictureUrl, picturePath), request.PictureTitle,
            request.PictureAlt, slug, request.Keywords, request.MetaDescription, request.CategoryId);
        products.Add(product);
        ProductPictures.AddAdditional(product, request.AdditionalPictures, picturePath, uploader);
        return operation.Succeeded();
    }
}
