using System.Security.Claims;
using _0_Framework.Application;
using Microsoft.AspNetCore.Authentication;

namespace OnlineShop.HttpContext;

public class HttpContextGetter(IHttpContextAccessor contextAccessor) : IHttpContextGetter
{
    public string? GetCurrentClaimType(string claimType)
	{
		return contextAccessor.HttpContext!.User.Claims.FirstOrDefault(a => a.Type == claimType)!.Value;
	}

	public bool IsAuthenticated()
	{
		return contextAccessor.HttpContext.User.Identity.IsAuthenticated;
	}
}