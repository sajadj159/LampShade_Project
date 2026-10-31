using MediatR;
using ShopManagement.Application.Contract.ProductCategory;
using LampShade.BuildingBlocks.Application;

namespace ShopManagement.Application.Contracts.Commands.ProductCategories.DeleteProductCategory;

public class DeleteProductCategoryCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
}
