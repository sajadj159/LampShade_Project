using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace _0_Framework.Application
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

       

        public List<int> GetPermissions()
        {
            if (!IsAuthenticated())
                return new List<int>();

            var permissions = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "permissions")
                ?.Value;
            return JsonConvert.DeserializeObject<List<int>>(permissions);
        }

        public long CurrentAccountId()
        {
            return IsAuthenticated() ? long.Parse(_contextAccessor.HttpContext.User.Claims.First(x => x.Type == "AccountId").Value) : 0;
        }

        public string CurrentAccountMobile()
        {
            return IsAuthenticated() ? _contextAccessor.HttpContext.User.Claims.First(x => x.Type == "Mobile").Value : "";

        }

        public string CurrentAccountRole()
        {
            return IsAuthenticated() ? _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value : null;
        }

        public AuthViewModel CurrentAccountInfo()
        {
            var result = new AuthViewModel();

            if (!IsAuthenticated())
            {
                return result;
            }

            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            result.Id = long.TryParse(claims.FirstOrDefault(x => x.Type == "AccountId")?.Value, out var accountId)
                ? accountId
                : 0;
            result.Username = claims.FirstOrDefault(x => x.Type == "Username")?.Value;
            result.Fullname = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            result.Mobile = claims.FirstOrDefault(x => x.Type == "Mobile")?.Value;
            result.RoleId = long.TryParse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value, out var roleId)
                ? roleId
                : 0;
            result.Role = claims.FirstOrDefault(x => x.Type == "RoleName")?.Value;

            var permissions = claims.FirstOrDefault(x => x.Type == "permissions")?.Value;
            result.Permissions = string.IsNullOrWhiteSpace(permissions)
                ? new List<int>()
                : JsonConvert.DeserializeObject<List<int>>(permissions) ?? new List<int>();

            return result;
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public void Signin(AuthViewModel account)
        {
            var permissions = JsonConvert.SerializeObject(account.Permissions);
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Fullname),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("Username", account.Username), // Or Use ClaimTypes.NameIdentifier
                new Claim("permissions",permissions),
                new Claim("Mobile", account.Mobile),
                new Claim("RoleName", account.Role ?? string.Empty),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties).GetAwaiter().GetResult();
        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).GetAwaiter().GetResult();
        }
    }
}
