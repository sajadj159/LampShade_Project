using AccountManagement.Application.Contracts.Commands.Accounts.ChangePassword;
using AccountManagement.Application.Contracts.Commands.Accounts.Edit;
using AccountManagement.Application.Contracts.Commands.Accounts.Login;
using AccountManagement.Application.Contracts.Commands.Accounts.Logout;
using AccountManagement.Application.Contracts.Commands.Accounts.MakeAddress;
using AccountManagement.Application.Contracts.Commands.Accounts.Register;
using LampShade.ReadModel.Contracts.Queries.Accounts.GetAccountById;
using LampShade.ReadModel.Contracts.Queries.Accounts.GetAccounts;
using LampShade.ReadModel.Contracts.Queries.Accounts.SearchAccounts;
using _0_Framework.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[ApiController]
[Route("api/write/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IAuthHelper _authHelper;

    public AccountController(IMediator mediator, IAuthHelper authHelper)
    {
        _mediator = mediator;
        _authHelper = authHelper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterCommand command)
        => Ok(await _mediator.Send(command));

    [Authorize]
    [HttpPut("edit")]
    public async Task<IActionResult> Edit([FromForm] EditAccountCommand command)
        => Ok(await _mediator.Send(command));

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        => Ok(await _mediator.Send(command));

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
        => Ok(await _mediator.Send(command));

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
        => Ok(await _mediator.Send(new LogoutCommand()));

    [Authorize]
    [HttpPost("address")]
    public async Task<IActionResult> MakeAddress([FromBody] MakeAddressCommand command)
    {
        command.AccountId = _authHelper.CurrentAccountId();
        return Ok(await _mediator.Send(command));
    }

    [Authorize]
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchAccountsQuery query)
        => Ok(await _mediator.Send(query));

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAccounts()
        => Ok(await _mediator.Send(new GetAccountsQuery()));

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(long id)
        => Ok(await _mediator.Send(new GetAccountByIdQuery { Id = id }));

    [Authorize]
    [HttpGet("{id}/address")]
    public async Task<IActionResult> GetAddressBy(long id)
    {
        var result = await _mediator.Send(new GetAccountByIdQuery { Id = id });
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id}/account")]
    public async Task<IActionResult> GetAccountBy(long id)
        => Ok(await _mediator.Send(new GetAccountByIdQuery { Id = id }));
}



