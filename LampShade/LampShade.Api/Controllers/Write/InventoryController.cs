using InventoryManagement.Application.Contracts.Commands.Inventories.CreateInventory;
using InventoryManagement.Application.Contracts.Commands.Inventories.EditInventory;
using InventoryManagement.Application.Contracts.Commands.Inventories.IncreaseInventory;
using InventoryManagement.Application.Contracts.Commands.Inventories.ReduceInventory;
using InventoryManagement.Application.Contracts.Commands.Inventories.ReduceInventoryRange;
using LampShade.ReadModel.Contracts.Queries.Inventories.GetInventoryById;
using LampShade.ReadModel.Contracts.Queries.Inventories.GetInventoryOperations;
using LampShade.ReadModel.Contracts.Queries.Inventories.SearchInventories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _0_Framework.Repository;

namespace LampShade.Api.Controllers.Write;

[Authorize(Roles = Roles.Administrator)]
[ApiController]
[Route("api/write/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IMediator _mediator;
    public InventoryController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create(CreateInventoryCommand command) => Ok(await _mediator.Send(command));

    [HttpPut]
    public async Task<IActionResult> Edit(EditInventoryCommand command) => Ok(await _mediator.Send(command));

    [HttpPost("increase")]
    public async Task<IActionResult> Increase(IncreaseInventoryCommand command) => Ok(await _mediator.Send(command));

    [HttpPost("reduce")]
    public async Task<IActionResult> Reduce(ReduceInventoryCommand command) => Ok(await _mediator.Send(command));

    [HttpPost("reduce-range")]
    public async Task<IActionResult> ReduceRange(ReduceInventoryRangeCommand command) => Ok(await _mediator.Send(command));

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchInventoriesQuery query) => Ok(await _mediator.Send(query));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(long id) => Ok(await _mediator.Send(new GetInventoryByIdQuery { Id = id }));

    [HttpGet("{id}/operations")]
    public async Task<IActionResult> GetOperationLog(long id) => Ok(await _mediator.Send(new GetInventoryOperationsQuery { InventoryId = id }));
}

