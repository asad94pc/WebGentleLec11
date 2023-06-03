using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Lec11.Service
{
    public class UserIdentityServices : IUserIdentityServices
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserIdentityServices(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string GetUserId()
        {
            var result = _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return result;
        }

        public bool IsAuthenticated()
        {
            var result = _httpContext.HttpContext.User.Identity.IsAuthenticated;
            return result;

        }
    }
}
