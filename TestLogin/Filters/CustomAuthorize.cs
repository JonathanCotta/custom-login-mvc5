using System.Web.Mvc;

namespace TestLogin.Filters
{
    public class CustomAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {    
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isLoged = (bool)filterContext.HttpContext.Session["isLoged"];
            var action = filterContext.ActionDescriptor.ActionName;
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            bool home = (action != "Index" || controller != "Home") ? true : false;
            bool register = (action != "Register" || controller != "Users") ? true : false;

            if (home && register)
            {
                if (!isLoged)
                {
                    filterContext.RequestContext.HttpContext.Response.Redirect("/Home/Index/",false);
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.ApplicationInstance.CompleteRequest();
                }
            }
        }
    }
}
