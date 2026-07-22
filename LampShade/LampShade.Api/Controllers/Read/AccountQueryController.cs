using _01_LampShadeQuery.Features.Accounts.Queries.GetAllAccountsForQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Read;

[ApiController]
[Route("api/read/[controller]")]
public class AccountQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public AccountQueryController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAccounts() => Ok(await _mediator.Send(new GetAllAccountsForQuery()));
}
