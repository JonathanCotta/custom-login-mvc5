using System.Web.Mvc;

namespace TestLogin.Filters
{
    public class CustomFilter : ActionFilterAttribute , IActionFilter
    {
        public string Role;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session["Role"];
            if (session != null)
            {
                bool sessionValue = (bool)session;
                string sessionRole = (sessionValue) ? "Admin" : "Normal";
                if (Role != sessionRole)
                {
                  filterContext.Result = new HttpUnauthorizedResult();
                }
            }
            else
            {
                filterContext.RequestContext.HttpContext.Response.Redirect("/Home/Index/", false);
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.ApplicationInstance.CompleteRequest();
            }
        }  
        
    }
}