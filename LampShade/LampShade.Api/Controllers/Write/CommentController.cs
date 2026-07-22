using CommentManagement.Application.Features.Comments.Commands.AddComment;
using CommentManagement.Application.Features.Comments.Commands.CancelComment;
using CommentManagement.Application.Features.Comments.Commands.ConfirmComment;
using CommentManagement.Application.Features.Comments.Queries.SearchComments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _0_Framework.Repository;

namespace LampShade.Api.Controllers.Write;

[Authorize]
[ApiController]
[Route("api/write/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;
    public CommentController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Add(AddCommentCommand command) => Ok(await _mediator.Send(command));

    [HttpGet("search")]
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> Search([FromQuery] SearchCommentsQuery query) => Ok(await _mediator.Send(query));

    [HttpPost("{id}/confirm")]
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> Confirm(long id) => Ok(await _mediator.Send(new ConfirmCommentCommand { Id = id }));

    [HttpPost("{id}/cancel")]
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> Cancel(long id) => Ok(await _mediator.Send(new CancelCommentCommand { Id = id }));
}
