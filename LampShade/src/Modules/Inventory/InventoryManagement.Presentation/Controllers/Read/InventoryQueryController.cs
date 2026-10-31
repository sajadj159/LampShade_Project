using System.Threading;
using System.Threading.Tasks;
using InventoryManagement.Application.ReadModel.Queries.Inventories.CheckStock;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Presentation.Controllers.Read;

[ApiController]
[Route("api/read/[controller]")]
public class InventoryQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public InventoryQueryController(IMediator mediator) => _mediator = mediator;

    [HttpPost("checkstock")]
    public async Task<IActionResult> CheckStock([FromBody] CheckStockQuery query) => Ok(await _mediator.Send(query));
}
