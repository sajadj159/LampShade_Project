using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.ProductPictures.Commands.CreateProductPicture;

public class CreateProductPictureCommand : IRequest<OperationResult>
{
    public long ProductId { get; set; }
    public IFormFile? PictureUrl { get; set; }
    public string PictureAlt { get; set; } = string.Empty;
    public string PictureTitle { get; set; } = string.Empty;
}

public class CreateProductPictureCommandHandler : IRequestHandler<CreateProductPictureCommand, OperationResult>
{
    private readonly IProductPictureApplication _application;

    public CreateProductPictureCommandHandler(IProductPictureApplication application)
    {
        _application = application;
    }

    public async Task<OperationResult> Handle(CreateProductPictureCommand request, CancellationToken cancellationToken)
    {
        var command = new ShopManagement.Application.Contract.A.ProductPicture.CreateProductPicture
        {
            ProductId = request.ProductId,
            PictureUrl = request.PictureUrl,
            PictureAlt = request.PictureAlt,
            PictureTitle = request.PictureTitle
        };
        return await Task.FromResult(_application.Create(command));
    }
}
