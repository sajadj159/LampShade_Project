using _01_LampShadeQuery.Features.Slides.Queries.GetSlidesForQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Read;

[ApiController]
[Route("api/read/[controller]")]
public class SlideQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public SlideQueryController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetSlides() => Ok(await _mediator.Send(new GetSlidesForQuery()));
}
