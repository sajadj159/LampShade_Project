using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.Queries.Products.CheckInventoryStatus;
using LampShade.ReadModel.Contracts.Queries.Products.GetLatestArrivals;
using LampShade.ReadModel.Contracts.Queries.Products.GetProductBySlug;
using LampShade.ReadModel.Contracts.Queries.Products.SearchProductsForQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Read;

[ApiController]
[Route("api/read/[controller]")]
public class ProductQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductQueryController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetProductDetails(string slug) => Ok(await _mediator.Send(new GetProductBySlugQuery { Slug = slug }));

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestArrivals() => Ok(await _mediator.Send(new GetLatestArrivalsQuery()));

    [HttpGet("search/{value}")]
    public async Task<IActionResult> Search(string value) => Ok(await _mediator.Send(new SearchProductsForQuery { Value = value }));

    [HttpPost("check-inventory")]
    public async Task<IActionResult> CheckInventoryStatus([FromBody] CheckInventoryStatusQuery query) => Ok(await _mediator.Send(query));
}
