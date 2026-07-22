using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;
using _0_Framework.Application;

using ShopManagement.Application.Contracts.Commands.ProductPictures.RestoreProductPicture;

namespace ShopManagement.Application.Features.ProductPictures.Commands.RestoreProductPicture;



public class RestoreProductPictureCommandHandler : IRequestHandler<RestoreProductPictureCommand, OperationResult>
{
    private readonly IProductPictureApplication _application;

    public RestoreProductPictureCommandHandler(IProductPictureApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(RestoreProductPictureCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.Restore(request.Id));
    }
}
