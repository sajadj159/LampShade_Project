using System.Threading;
using System.Threading.Tasks;
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

    public async Task<OperationResult> Handle(RestoreProductPictureCommand request, CancellationToken cancellationToken)
    {
        return await _application.RestoreAsync(request.Id, cancellationToken);
    }
}
