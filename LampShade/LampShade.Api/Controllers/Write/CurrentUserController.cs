using _0_Framework.Application;
using AccountManagement.Application.Features.Accounts.Queries.GetAccountById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[ApiController]
[Route("api/write/[controller]")]
public class CurrentUserController : ControllerBase
{
    private readonly IAuthHelper _authHelper;
    private readonly IMediator _mediator;

    public CurrentUserController(IAuthHelper authHelper, IMediator mediator)
    {
        _authHelper = authHelper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrentUserInfo(CancellationToken cancellationToken)
    {
        if (!_authHelper.IsAuthenticated())
            return Unauthorized();

        var info = _authHelper.CurrentAccountInfo();
        var account = await _mediator.Send(new GetAccountByIdQuery { Id = info.Id }, cancellationToken);

        return Ok(new
        {
            Id = account?.Id > 0 ? account.Id : info.Id,
            Username = string.IsNullOrWhiteSpace(account?.UserName) ? info.Username : account.UserName,
            Fullname = string.IsNullOrWhiteSpace(account?.FullName) ? info.Fullname : account.FullName,
            Mobile = string.IsNullOrWhiteSpace(account?.Mobile) ? info.Mobile : account.Mobile,
            Address = account?.Address ?? string.Empty,
            PostalCode = account?.PostalCode ?? string.Empty,
            RoleId = account?.RoleId > 0 ? account.RoleId : info.RoleId,
            Role = string.IsNullOrWhiteSpace(account?.Role) ? info.Role : account.Role,
            ProfilePhoto = string.IsNullOrWhiteSpace(account?.ProfilePhoto) ? info.ProfilePhoto : account.ProfilePhoto,
            info.Permissions
        });
    }
}

