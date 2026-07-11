using _0_Framework.Application;
using AccountManagement.Application.Contracts.AC.Account;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[ApiController]
[Route("api/write/[controller]")]
public class CurrentUserController : ControllerBase
{
    private readonly IAuthHelper _authHelper;
    private readonly IAccountApplication _accountApplication;

    public CurrentUserController(IAuthHelper authHelper, IAccountApplication accountApplication)
    {
        _authHelper = authHelper;
        _accountApplication = accountApplication;
    }

    [HttpGet]
    public IActionResult GetCurrentUserInfo()
    {
        if (!_authHelper.IsAuthenticated())
            return Unauthorized();

        var info = _authHelper.CurrentAccountInfo();
        var account = _accountApplication.GetAccountBy(info.Id);

        return Ok(new
        {
            Id = account?.Id > 0 ? account.Id : info.Id,
            Username = string.IsNullOrWhiteSpace(account?.UserName) ? info.Username : account.UserName,
            Fullname = string.IsNullOrWhiteSpace(account?.FullName) ? info.Fullname : account.FullName,
            Mobile = string.IsNullOrWhiteSpace(account?.Mobile) ? info.Mobile : account.Mobile,
            RoleId = account?.RoleId > 0 ? account.RoleId : info.RoleId,
            Role = string.IsNullOrWhiteSpace(account?.Role) ? info.Role : account.Role,
            ProfilePhoto = string.IsNullOrWhiteSpace(account?.ProfilePhoto) ? info.ProfilePhoto : account.ProfilePhoto,
            info.Permissions
        });
    }
}
