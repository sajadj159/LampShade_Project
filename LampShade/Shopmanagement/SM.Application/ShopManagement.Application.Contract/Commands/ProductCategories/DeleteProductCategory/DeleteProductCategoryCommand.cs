using MediatR;
using ShopManagement.Application.Contract.ProductCategory;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Commands.ProductCategories.DeleteProductCategory;

public class DeleteProductCategoryCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
}
