using LampShade.Api.Features.Comments.Commands.AddComment;
using LampShade.Api.Features.Comments.Commands.CancelComment;
using LampShade.Api.Features.Comments.Commands.ConfirmComment;
using LampShade.Api.Features.Comments.Queries.SearchComments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[Authorize]
[ApiController]
[Route("api/write/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;
    public CommentController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Add(AddCommentCommand command) => Ok(await _mediator.Send(command));

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchCommentsQuery query) => Ok(await _mediator.Send(query));

    [HttpPost("{id}/confirm")]
    public async Task<IActionResult> Confirm(long id) => Ok(await _mediator.Send(new ConfirmCommentCommand { Id = id }));

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> Cancel(long id) => Ok(await _mediator.Send(new CancelCommentCommand { Id = id }));
}
