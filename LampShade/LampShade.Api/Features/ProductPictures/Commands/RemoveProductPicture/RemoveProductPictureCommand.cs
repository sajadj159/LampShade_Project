using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.ProductPictures.Commands.RemoveProductPicture;

public class RemoveProductPictureCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
}

public class RemoveProductPictureCommandHandler : IRequestHandler<RemoveProductPictureCommand, OperationResult>
{
    private readonly IProductPictureApplication _application;

    public RemoveProductPictureCommandHandler(IProductPictureApplication application)
    {
        _application = application;
    }

    public async Task<OperationResult> Handle(RemoveProductPictureCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_application.Remove(request.Id));
    }
}
