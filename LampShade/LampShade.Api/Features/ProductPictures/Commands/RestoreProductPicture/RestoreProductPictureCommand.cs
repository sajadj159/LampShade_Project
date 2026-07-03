using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.ProductPictures.Commands.RestoreProductPicture;

public class RestoreProductPictureCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
}

public class RestoreProductPictureCommandHandler : IRequestHandler<RestoreProductPictureCommand, OperationResult>
{
    private readonly IProductPictureApplication _application;

    public RestoreProductPictureCommandHandler(IProductPictureApplication application)
    {
        _application = application;
    }

    public async Task<OperationResult> Handle(RestoreProductPictureCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_application.Restore(request.Id));
    }
}
