using BlogManagement.Application.Contracts.Commands.ArticleCategories.CreateArticleCategory;
using BlogManagement.Application.Contracts.Commands.ArticleCategories.EditArticleCategory;
using LampShade.ReadModel.Contracts.Queries.ArticleCategories.GetArticleCategories;
using LampShade.ReadModel.Contracts.Queries.ArticleCategories.GetArticleCategoryById;
using LampShade.ReadModel.Contracts.Queries.ArticleCategories.SearchArticleCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[Authorize]
[ApiController]
[Route("api/write/[controller]")]
public class ArticleCategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    public ArticleCategoryController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateArticleCategoryCommand command) => Ok(await _mediator.Send(command));

    [HttpPut]
    public async Task<IActionResult> Edit([FromForm] EditArticleCategoryCommand command) => Ok(await _mediator.Send(command));

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchArticleCategoriesQuery query) => Ok(await _mediator.Send(query));

    [HttpGet]
    public async Task<IActionResult> GetArticleCategories() => Ok(await _mediator.Send(new GetArticleCategoriesQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(long id) => Ok(await _mediator.Send(new GetArticleCategoryByIdQuery { Id = id }));
}

