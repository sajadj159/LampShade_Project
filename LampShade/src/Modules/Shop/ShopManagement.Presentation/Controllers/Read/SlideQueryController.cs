using System.Threading;
using System.Threading.Tasks;
using ShopManagement.Application.ReadModel.Queries.Slides.GetSlidesForQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShopManagement.Presentation.Controllers.Read;

[ApiController]
[Route("api/read/[controller]")]
public class SlideQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public SlideQueryController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetSlides() => Ok(await _mediator.Send(new GetSlidesForQuery()));
}
