#nullable enable

using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

using ShopManagement.Application.Contracts.Commands.ProductPictures.CreateProductPicture;

namespace ShopManagement.Application.Features.ProductPictures.Commands.CreateProductPicture;



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
