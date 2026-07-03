using LampShade.Api.Features.ProductCategories.Commands.CreateProductCategory;
using LampShade.Api.Features.ProductCategories.Commands.EditProductCategory;
using LampShade.Api.Features.ProductCategories.Queries.GetProductCategories;
using LampShade.Api.Features.ProductCategories.Queries.GetProductCategoryById;
using LampShade.Api.Features.ProductCategories.Queries.SearchProductCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[ApiController]
[Route("api/write/[controller]")]
public class ProductCategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductCategoryController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProductCategoryCommand command) => Ok(await _mediator.Send(command));

    [HttpPut]
    public async Task<IActionResult> Edit([FromForm] EditProductCategoryCommand command) => Ok(await _mediator.Send(command));

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchProductCategoriesQuery query) => Ok(await _mediator.Send(query));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(long id) => Ok(await _mediator.Send(new GetProductCategoryByIdQuery { Id = id }));

    [HttpGet]
    public async Task<IActionResult> GetProductCategories() => Ok(await _mediator.Send(new GetProductCategoriesQuery()));
}
