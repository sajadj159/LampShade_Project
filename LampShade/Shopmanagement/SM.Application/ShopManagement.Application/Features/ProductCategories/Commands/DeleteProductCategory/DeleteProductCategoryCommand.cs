using MediatR;
using ShopManagement.Application.Contract.ProductCategory;
using _0_Framework.Application;

namespace ShopManagement.Application.Features.ProductCategories.Commands.DeleteProductCategory;

public class DeleteProductCategoryCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
}

public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, OperationResult>
{
    private readonly IProductCategoryApplication _application;

    public DeleteProductCategoryCommandHandler(IProductCategoryApplication application) => _application = application;

    public Task<OperationResult> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
        => Task.FromResult(_application.Delete(request.Id));
}
