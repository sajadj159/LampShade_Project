#nullable enable

using ShopManagement.Application.Contract.A.Slide;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

using ShopManagement.Application.Contracts.Commands.Slides.CreateSlide;

namespace ShopManagement.Application.Features.Slides.Commands.CreateSlide;



public class CreateSlideCommandHandler : IRequestHandler<CreateSlideCommand, OperationResult>
{
    private readonly ISlideApplication _application;

    public CreateSlideCommandHandler(ISlideApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(CreateSlideCommand request, CancellationToken cancellationToken)
    {
        var command = new ShopManagement.Application.Contract.A.Slide.CreateSlide
        {
            PictureUrl = request.PictureUrl,
            PictureTitle = request.PictureTitle,
            PictureAlt = request.PictureAlt,
            Heading = request.Heading,
            Title = request.Title,
            Text = request.Text,
            Link = request.Link,
            BtnText = request.BtnText
        };
        return Task.FromResult(_application.Create(command));
    }
}
