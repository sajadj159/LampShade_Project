using LampShade.Api.Features.ArticleCategories.Queries.GetArticleCategoriesForQuery;
using LampShade.Api.Features.ArticleCategories.Queries.GetArticleCategoryBySlug;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Read;

[ApiController]
[Route("api/read/[controller]")]
public class ArticleCategoryQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public ArticleCategoryQueryController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetArticleCategory(string slug) => Ok(await _mediator.Send(new GetArticleCategoryBySlugQuery { Slug = slug }));

    [HttpGet]
    public async Task<IActionResult> GetArticleCategories() => Ok(await _mediator.Send(new GetArticleCategoriesForQuery()));
}
