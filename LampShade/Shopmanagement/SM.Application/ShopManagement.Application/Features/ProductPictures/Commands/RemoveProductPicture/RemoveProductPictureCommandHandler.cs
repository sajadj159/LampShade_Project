using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;
using _0_Framework.Application;

using ShopManagement.Application.Contracts.Commands.ProductPictures.RemoveProductPicture;

namespace ShopManagement.Application.Features.ProductPictures.Commands.RemoveProductPicture;



public class RemoveProductPictureCommandHandler : IRequestHandler<RemoveProductPictureCommand, OperationResult>
{
    private readonly IProductPictureApplication _application;

    public RemoveProductPictureCommandHandler(IProductPictureApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(RemoveProductPictureCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.Remove(request.Id));
    }
}
