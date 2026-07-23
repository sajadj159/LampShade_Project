using System.Threading;
using System.Threading.Tasks;
#nullable enable

using ShopManagement.Application.Contract.A.Slide;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

using ShopManagement.Application.Contracts.Commands.Slides.EditSlide;

namespace ShopManagement.Application.Features.Slides.Commands.EditSlide;



public class EditSlideCommandHandler : IRequestHandler<EditSlideCommand, OperationResult>
{
    private readonly ISlideApplication _application;

    public EditSlideCommandHandler(ISlideApplication application)
    {
        _application = application;
    }

    public async Task<OperationResult> Handle(EditSlideCommand request, CancellationToken cancellationToken)
    {
        var command = new ShopManagement.Application.Contract.A.Slide.EditSlide
        {
            Id = request.Id,
            PictureUrl = request.PictureUrl,
            PictureTitle = request.PictureTitle,
            PictureAlt = request.PictureAlt,
            Heading = request.Heading,
            Title = request.Title,
            Text = request.Text,
            Link = request.Link,
            BtnText = request.BtnText
        };
        return await _application.EditAsync(command, cancellationToken);
    }
}
