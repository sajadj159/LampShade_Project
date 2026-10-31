using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.ProductCategories.EditProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.Features.ProductCategories.Commands.EditProductCategory;

public class EditProductCategoryCommandHandler(IProductCategoryRepository categories, IFileUploader uploader)
    : IRequestHandler<EditProductCategoryCommand, OperationResult>
{
    public async Task<OperationResult> Handle(EditProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var category = await categories.GetAsync(request.Id, cancellationToken);
        if (category is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (await categories.ExistAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        category.Edit(request.Name, request.Description, uploader.Upload(request.PictureUrl, request.Slug), request.PictureAlt,
            request.PictureTitle, request.Keywords, request.MetaDescription, request.Slug.Slugify());
        return operation.Succeeded();
    }
}
