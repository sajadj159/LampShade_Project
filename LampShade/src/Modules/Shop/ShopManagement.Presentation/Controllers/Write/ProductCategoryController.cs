using System.Threading;
using System.Threading.Tasks;
using ShopManagement.Application.Contracts.Commands.ProductCategories.CreateProductCategory;
using ShopManagement.Application.Contracts.Commands.ProductCategories.EditProductCategory;
using ShopManagement.Application.Contracts.Commands.ProductCategories.DeleteProductCategory;
using LampShade.ReadModel.Contracts.Queries.ProductCategories.GetProductCategories;
using LampShade.ReadModel.Contracts.Queries.ProductCategories.GetProductCategoryById;
using LampShade.ReadModel.Contracts.Queries.ProductCategories.SearchProductCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _0_Framework.Repository;

namespace LampShade.Api.Controllers.Write;

[Authorize(Roles = Roles.Administrator)]
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

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id) => Ok(await _mediator.Send(new DeleteProductCategoryCommand { Id = id }));

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchProductCategoriesQuery query) => Ok(await _mediator.Send(query));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(long id) => Ok(await _mediator.Send(new GetProductCategoryByIdQuery { Id = id }));

    [HttpGet]
    public async Task<IActionResult> GetProductCategories() => Ok(await _mediator.Send(new GetProductCategoriesQuery()));
}
