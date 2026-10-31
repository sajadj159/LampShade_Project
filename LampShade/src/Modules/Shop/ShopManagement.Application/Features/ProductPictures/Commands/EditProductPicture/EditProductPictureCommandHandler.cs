using System.Threading;
using System.Threading.Tasks;
#nullable enable

using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

using ShopManagement.Application.Contracts.Commands.ProductPictures.EditProductPicture;

namespace ShopManagement.Application.Features.ProductPictures.Commands.EditProductPicture;



public class EditProductPictureCommandHandler : IRequestHandler<EditProductPictureCommand, OperationResult>
{
    private readonly IProductPictureApplication _application;

    public EditProductPictureCommandHandler(IProductPictureApplication application)
    {
        _application = application;
    }

    public async Task<OperationResult> Handle(EditProductPictureCommand request, CancellationToken cancellationToken)
    {
        var command = new ShopManagement.Application.Contract.A.ProductPicture.EditProductPicture
        {
            Id = request.Id,
            ProductId = request.ProductId,
            PictureUrl = request.PictureUrl,
            PictureAlt = request.PictureAlt,
            PictureTitle = request.PictureTitle
        };
        return await _application.EditAsync(command, cancellationToken);
    }
}
