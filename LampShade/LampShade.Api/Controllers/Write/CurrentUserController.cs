using _0_Framework.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[ApiController]
[Route("api/write/[controller]")]
public class CurrentUserController : ControllerBase
{
    private readonly IAuthHelper _authHelper;

    public CurrentUserController(IAuthHelper authHelper)
    {
        _authHelper = authHelper;
    }

    [HttpGet]
    public IActionResult GetCurrentUserInfo()
    {
        if (!_authHelper.IsAuthenticated())
            return Unauthorized();

        var info = _authHelper.CurrentAccountInfo();
        return Ok(new
        {
            info.Id,
            info.Username,
            info.Fullname,
            info.Mobile,
            info.RoleId,
            info.Role,
            info.Permissions
        });
    }
}
