using LampShade.Api.Features.ProductCategories.Queries.GetProductCategoriesWithProductsForQuery;
using LampShade.Api.Features.ProductCategories.Queries.GetProductCategoryWithProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Read;

[ApiController]
[Route("api/read/[controller]")]
public class ProductCategoryQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductCategoryQueryController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetProductCategoryWithProducts(string slug) => Ok(await _mediator.Send(new GetProductCategoryWithProductsQuery { Slug = slug }));

    [HttpGet]
    public async Task<IActionResult> GetProductCategoryQueries() => Ok(await _mediator.Send(new GetProductCategoriesWithProductsForQuery()));

    [HttpGet("with-products")]
    public async Task<IActionResult> GetProductCategoriesWithProducts() => Ok(await _mediator.Send(new GetProductCategoriesWithProductsForQuery()));
}
