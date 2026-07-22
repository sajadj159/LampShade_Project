using _01_LampShadeQuery.Features.Articles.Queries.GetArticleBySlug;
using _01_LampShadeQuery.Features.Articles.Queries.LatestArticles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Read;

[ApiController]
[Route("api/read/[controller]")]
public class ArticleQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public ArticleQueryController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{value}")]
    public async Task<IActionResult> GetArticleDetails(string value) => Ok(await _mediator.Send(new GetArticleBySlugQuery { Slug = value }));

    [HttpGet("latest")]
    public async Task<IActionResult> LatestArticles() => Ok(await _mediator.Send(new LatestArticlesQuery()));
}
