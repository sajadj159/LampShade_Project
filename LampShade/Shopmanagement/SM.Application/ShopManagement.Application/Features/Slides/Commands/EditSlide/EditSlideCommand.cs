#nullable enable

using ShopManagement.Application.Contract.A.Slide;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

namespace ShopManagement.Application.Features.Slides.Commands.EditSlide;

public class EditSlideCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
    public IFormFile? PictureUrl { get; set; }
    public string PictureTitle { get; set; } = string.Empty;
    public string PictureAlt { get; set; } = string.Empty;
    public string Heading { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string BtnText { get; set; } = string.Empty;
}

public class EditSlideCommandHandler : IRequestHandler<EditSlideCommand, OperationResult>
{
    private readonly ISlideApplication _application;

    public EditSlideCommandHandler(ISlideApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(EditSlideCommand request, CancellationToken cancellationToken)
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
        return Task.FromResult(_application.Edit(command));
    }
}
