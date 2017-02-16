using System.Web.Mvc;
using TestLogin.Filters;

namespace TestLogin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        } 
       
        [AccessFilter("Admin")]
        public ActionResult About()
        {   
            return View();
        }

        public ActionResult Contact()
        {   
            return View();
        }
    }
}