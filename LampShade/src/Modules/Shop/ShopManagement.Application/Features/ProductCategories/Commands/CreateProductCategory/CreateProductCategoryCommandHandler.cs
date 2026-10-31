using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.ProductCategories.CreateProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.Features.ProductCategories.Commands.CreateProductCategory;

public class CreateProductCategoryCommandHandler(IProductCategoryRepository categories, IFileUploader uploader)
    : IRequestHandler<CreateProductCategoryCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (await categories.ExistAsync(x => x.Name == request.Name, cancellationToken))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var slug = request.Slug.Slugify();
        categories.Add(new Domain.ProductCategoryAgg.ProductCategory(request.Name, request.Description,
            uploader.Upload(request.PictureUrl, request.Slug), request.PictureAlt, request.PictureTitle, request.Keywords,
            request.MetaDescription, slug));
        return operation.Succeeded();
    }
}
