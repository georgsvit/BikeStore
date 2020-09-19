using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BikeStore.Filter
{
    public class NotForCustomerFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.IsInRole("Admin") && !context.HttpContext.User.IsInRole("Seller"))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
