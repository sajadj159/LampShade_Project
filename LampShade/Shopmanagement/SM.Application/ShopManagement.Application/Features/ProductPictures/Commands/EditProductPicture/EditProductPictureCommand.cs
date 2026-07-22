#nullable enable

using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

namespace ShopManagement.Application.Features.ProductPictures.Commands.EditProductPicture;

public class EditProductPictureCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public IFormFile? PictureUrl { get; set; }
    public string PictureAlt { get; set; } = string.Empty;
    public string PictureTitle { get; set; } = string.Empty;
}

public class EditProductPictureCommandHandler : IRequestHandler<EditProductPictureCommand, OperationResult>
{
    private readonly IProductPictureApplication _application;

    public EditProductPictureCommandHandler(IProductPictureApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(EditProductPictureCommand request, CancellationToken cancellationToken)
    {
        var command = new ShopManagement.Application.Contract.A.ProductPicture.EditProductPicture
        {
            Id = request.Id,
            ProductId = request.ProductId,
            PictureUrl = request.PictureUrl,
            PictureAlt = request.PictureAlt,
            PictureTitle = request.PictureTitle
        };
        return Task.FromResult(_application.Edit(command));
    }
}
