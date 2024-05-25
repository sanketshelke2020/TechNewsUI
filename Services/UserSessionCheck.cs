using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TechNewsUI.Services
{
    public class UserSessionCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            string userId = session.GetString("UserId");
            string role = session.GetString("Role");
            if (userId == null || role == "Admin")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        {"Controller", "Login" },
                        { "Action", "Login" }
                    });
            }
        }
    }
}
