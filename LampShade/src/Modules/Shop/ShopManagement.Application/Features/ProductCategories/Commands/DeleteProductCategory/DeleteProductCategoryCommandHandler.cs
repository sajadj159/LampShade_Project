using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.ProductCategories.DeleteProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.Features.ProductCategories.Commands.DeleteProductCategory;

public class DeleteProductCategoryCommandHandler(IProductCategoryRepository categories) : IRequestHandler<DeleteProductCategoryCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var category = await categories.GetAsync(request.Id, cancellationToken);
        if (category is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (await categories.HasProductsAsync(request.Id, cancellationToken))
            return operation.Failed("A category with products cannot be deleted.");

        await categories.RemoveAsync(category, cancellationToken);
        return operation.Succeeded();
    }
}
