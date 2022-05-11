using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Auth
{
    public class IdentityServiceImpl : IIdentityService
    {
        private readonly IHttpContextAccessor _accessor;

        public IdentityServiceImpl(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string GetUsername()
        {
            //User.FindFirstValue(ClaimTypes.NameIdentifier);
            //return _accessor.HttpContext.User?.Identity?.Name;
            var m = _accessor.HttpContext.User;
            return _accessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
