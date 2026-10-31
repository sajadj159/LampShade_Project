using LampShade.BuildingBlocks.Application;
using System.Threading;
using System.Threading.Tasks;
using BlogManagement.Application.Contracts.Commands.Articles.CreateArticle;
using BlogManagement.Application.Contracts.Commands.Articles.EditArticle;
using BlogManagement.Application.ReadModel.Queries.Articles.GetArticleById;
using BlogManagement.Application.ReadModel.Queries.Articles.SearchArticles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.Presentation.Controllers.Write;

[Authorize(Roles = Roles.Administrator)]
[ApiController]
[Route("api/write/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly IMediator _mediator;
    public ArticleController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateArticleCommand command) => Ok(await _mediator.Send(command));

    [HttpPut]
    public async Task<IActionResult> Edit([FromForm] EditArticleCommand command) => Ok(await _mediator.Send(command));

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchArticlesQuery query) => Ok(await _mediator.Send(query));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(long id) => Ok(await _mediator.Send(new GetArticleByIdQuery { Id = id }));
}
