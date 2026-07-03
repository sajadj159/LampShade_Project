using LampShade.Api.Features.Roles.Commands.CreateRole;
using LampShade.Api.Features.Roles.Commands.EditRole;
using LampShade.Api.Features.Roles.Queries.GetRoleById;
using LampShade.Api.Features.Roles.Queries.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[Authorize]
[ApiController]
[Route("api/write/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;
    public RoleController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleCommand command) => Ok(await _mediator.Send(command));

    [HttpPut]
    public async Task<IActionResult> Edit(EditRoleCommand command) => Ok(await _mediator.Send(command));

    [HttpGet]
    public async Task<IActionResult> GetRoles() => Ok(await _mediator.Send(new GetRolesQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(long id) => Ok(await _mediator.Send(new GetRoleByIdQuery { Id = id }));
}
