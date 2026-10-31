using LampShade.BuildingBlocks.Application;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using System.Threading.Tasks;
using AccountManagement.Application.ReadModel.Queries.Accounts.GetAllAccountsForQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Presentation.Controllers.Read;

[Authorize(Roles = Roles.Administrator)]
[ApiController]
[Route("api/read/[controller]")]
public class AccountQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public AccountQueryController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAccounts() => Ok(await _mediator.Send(new GetAllAccountsForQuery()));
}
