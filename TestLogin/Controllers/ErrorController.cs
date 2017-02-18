using System.Web.Mvc;

namespace TestLogin.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error        
        public ActionResult NotFound()
        {                  
            return View();
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult ServerError()
        {
            return View();
        }
            
    }
}