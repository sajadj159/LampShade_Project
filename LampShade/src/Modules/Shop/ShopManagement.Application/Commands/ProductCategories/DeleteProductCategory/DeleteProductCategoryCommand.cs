using MediatR;
using ShopManagement.Application.Contract.ProductCategory;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Commands.ProductCategories.DeleteProductCategory;

public class DeleteProductCategoryCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
}
