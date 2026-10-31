using LampShade.BuildingBlocks.Application;
using System.Threading;
using System.Threading.Tasks;
using BlogManagement.Application.Contracts.Commands.ArticleCategories.CreateArticleCategory;
using BlogManagement.Application.Contracts.Commands.ArticleCategories.EditArticleCategory;
using BlogManagement.Application.ReadModel.Queries.ArticleCategories.GetArticleCategories;
using BlogManagement.Application.ReadModel.Queries.ArticleCategories.GetArticleCategoryById;
using BlogManagement.Application.ReadModel.Queries.ArticleCategories.SearchArticleCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.Presentation.Controllers.Write;

[Authorize(Roles = Roles.Administrator)]
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
