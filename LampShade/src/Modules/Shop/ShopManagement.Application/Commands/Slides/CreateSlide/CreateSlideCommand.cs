#nullable enable

using ShopManagement.Application.Contract.A.Slide;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Commands.Slides.CreateSlide;

public class CreateSlideCommand : ICommand<OperationResult>
{
    public IFormFile? PictureUrl { get; set; }
    public string PictureTitle { get; set; } = string.Empty;
    public string PictureAlt { get; set; } = string.Empty;
    public string Heading { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string BtnText { get; set; } = string.Empty;
}
