using System.Threading;
using System.Threading.Tasks;
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

    public async Task<OperationResult> Handle(RemoveProductPictureCommand request, CancellationToken cancellationToken)
    {
        return await _application.RemoveAsync(request.Id, cancellationToken);
    }
}
