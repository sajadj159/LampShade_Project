using MediatR;
using ShopManagement.Application.Contract.ProductCategory;
using _0_Framework.Application;

using ShopManagement.Application.Contracts.Commands.ProductCategories.DeleteProductCategory;

namespace ShopManagement.Application.Features.ProductCategories.Commands.DeleteProductCategory;



public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, OperationResult>
{
    private readonly IProductCategoryApplication _application;

    public DeleteProductCategoryCommandHandler(IProductCategoryApplication application) => _application = application;

    public Task<OperationResult> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
        => Task.FromResult(_application.Delete(request.Id));
}
