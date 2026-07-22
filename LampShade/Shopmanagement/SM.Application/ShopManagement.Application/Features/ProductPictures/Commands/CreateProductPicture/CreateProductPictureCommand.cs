#nullable enable

using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

namespace ShopManagement.Application.Features.ProductPictures.Commands.CreateProductPicture;

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

    public Task<OperationResult> Handle(CreateProductPictureCommand request, CancellationToken cancellationToken)
    {
        var command = new ShopManagement.Application.Contract.A.ProductPicture.CreateProductPicture
        {
            ProductId = request.ProductId,
            PictureUrl = request.PictureUrl,
            PictureAlt = request.PictureAlt,
            PictureTitle = request.PictureTitle
        };
        return Task.FromResult(_application.Create(command));
    }
}
