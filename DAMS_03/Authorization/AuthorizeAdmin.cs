//using System;
using System.Web.Mvc;

namespace DAMS_03.Authorization
{
    public class AuthorizeAdmin : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // If they are authorized, handle accordingly
            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                // Otherwise redirect to your specific authorized area
                filterContext.Result = new RedirectResult("~/Account/Unauthorized");
            }
        }
    }
}