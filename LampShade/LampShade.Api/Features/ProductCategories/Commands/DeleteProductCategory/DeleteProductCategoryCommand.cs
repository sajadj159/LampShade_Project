using MediatR;
using ShopManagement.Application.Contract.ProductCategory;
using _0_Framework.Application;

namespace LampShade.Api.Features.ProductCategories.Commands.DeleteProductCategory;

public class DeleteProductCategoryCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
}

public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, OperationResult>
{
    private readonly IProductCategoryApplication _application;

    public DeleteProductCategoryCommandHandler(IProductCategoryApplication application) => _application = application;

    public async Task<OperationResult> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
        => await Task.FromResult(_application.Delete(request.Id));
}
