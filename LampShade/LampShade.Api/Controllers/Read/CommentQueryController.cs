using _01_LampShadeQuery.Features.Comments.Queries.GetAllComments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Read;

[ApiController]
[Route("api/read/[controller]")]
public class CommentQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public CommentQueryController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetComments() => Ok(await _mediator.Send(new GetAllCommentsQuery()));
}
