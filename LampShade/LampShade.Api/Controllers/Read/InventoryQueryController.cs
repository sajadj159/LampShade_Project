using LampShade.ReadModel.Contracts.Queries.Inventories.CheckStock;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Read;

[ApiController]
[Route("api/read/[controller]")]
public class InventoryQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public InventoryQueryController(IMediator mediator) => _mediator = mediator;

    [HttpPost("checkstock")]
    public async Task<IActionResult> CheckStock([FromBody] CheckStockQuery query) => Ok(await _mediator.Send(query));
}

