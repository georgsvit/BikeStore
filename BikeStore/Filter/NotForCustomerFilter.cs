using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
