using Microsoft.AspNetCore.Mvc.Filters;

namespace RideHailing.API.Filter
{
    public class AuthorizationFilterAttr : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
