using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application
{
    public class HttpContextGetter : IHttpContextGetter
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextGetter(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetCurrentClaimType(string claimType)
        {
            return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == claimType).Value;
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
