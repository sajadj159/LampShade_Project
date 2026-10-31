using LampShade.BuildingBlocks.Application;
using System.Threading;
using System.Threading.Tasks;
using ShopManagement.Application.Contracts.Commands.Products.CreateProduct;
using ShopManagement.Application.Contracts.Commands.Products.EditProduct;
using ShopManagement.Application.ReadModel.Queries.Products.GetProductById;
using ShopManagement.Application.ReadModel.Queries.Products.GetProducts;
using ShopManagement.Application.ReadModel.Queries.Products.SearchProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopManagement.Presentation.Controllers.Write;

[Authorize(Roles = Roles.Administrator)]
[ApiController]
[Route("api/write/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProductCommand command) => Ok(await _mediator.Send(command));

    [HttpPut]
    public async Task<IActionResult> Edit([FromForm] EditProductCommand command) => Ok(await _mediator.Send(command));

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchProductsQuery query) => Ok(await _mediator.Send(query));

    [HttpGet]
    public async Task<IActionResult> GetProducts() => Ok(await _mediator.Send(new GetProductsQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(long id) => Ok(await _mediator.Send(new GetProductByIdQuery { Id = id }));
}
