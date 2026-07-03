using LampShade.Api.Features.Articles.Commands.CreateArticle;
using LampShade.Api.Features.Articles.Commands.EditArticle;
using LampShade.Api.Features.Articles.Queries.GetArticleById;
using LampShade.Api.Features.Articles.Queries.SearchArticles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

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
