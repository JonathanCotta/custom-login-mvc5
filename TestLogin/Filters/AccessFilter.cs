using System.Web.Mvc;
using System.Web.Routing;

namespace TestLogin.Filters
{
    public class AccessFilter : ActionFilterAttribute
    {
        public string[] Role;

        public AccessFilter(params string[] role) { Role = role; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string sessionRole = null;

            if (filterContext.HttpContext.Session["Role"] != null)
                sessionRole = (bool)filterContext.HttpContext.Session["Role"] ? "Admin" : "Normal";

            if (sessionRole != null)
            {
                var redirect = true;
                foreach (var r in Role)
                {
                    if (r.Equals(sessionRole))
                        redirect = false;
                    break;
                }
                if (redirect)
                    filterContext.Result = new HttpUnauthorizedResult();
            }
            else
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } }); 
        }  
        
    }
}