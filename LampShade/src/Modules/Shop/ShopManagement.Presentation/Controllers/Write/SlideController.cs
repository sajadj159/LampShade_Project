using LampShade.BuildingBlocks.Application;
using System.Threading;
using System.Threading.Tasks;
using ShopManagement.Application.Contracts.Commands.Slides.CreateSlide;
using ShopManagement.Application.Contracts.Commands.Slides.EditSlide;
using ShopManagement.Application.Contracts.Commands.Slides.RemoveSlide;
using ShopManagement.Application.Contracts.Commands.Slides.RestoreSlide;
using ShopManagement.Application.ReadModel.Queries.Slides.GetSlideById;
using ShopManagement.Application.ReadModel.Queries.Slides.GetSlides;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopManagement.Presentation.Controllers.Write;

[Authorize(Roles = Roles.Administrator)]
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
