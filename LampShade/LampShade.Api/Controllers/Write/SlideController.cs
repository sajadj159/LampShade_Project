using LampShade.Api.Features.Slides.Commands.CreateSlide;
using LampShade.Api.Features.Slides.Commands.EditSlide;
using LampShade.Api.Features.Slides.Commands.RemoveSlide;
using LampShade.Api.Features.Slides.Commands.RestoreSlide;
using LampShade.Api.Features.Slides.Queries.GetSlideById;
using LampShade.Api.Features.Slides.Queries.GetSlides;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[ApiController]
[Route("api/write/[controller]")]
public class SlideController : ControllerBase
{
    private readonly IMediator _mediator;
    public SlideController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateSlideCommand command) => Ok(await _mediator.Send(command));

    [HttpPut]
    public async Task<IActionResult> Edit([FromForm] EditSlideCommand command) => Ok(await _mediator.Send(command));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(long id) => Ok(await _mediator.Send(new RemoveSlideCommand { Id = id }));

    [HttpPost("{id}/restore")]
    public async Task<IActionResult> Restore(long id) => Ok(await _mediator.Send(new RestoreSlideCommand { Id = id }));

    [HttpGet]
    public async Task<IActionResult> GetList() => Ok(await _mediator.Send(new GetSlidesQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(long id) => Ok(await _mediator.Send(new GetSlideByIdQuery { Id = id }));
}
