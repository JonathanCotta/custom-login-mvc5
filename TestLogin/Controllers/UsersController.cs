using System.Threading.Tasks;
using System.Web.Mvc;
using TestLogin.Filters;
using TestLogin.Models;
using TestLogin.Models.Repository;


namespace TestLogin.Controllers
{
    [Authorization]
    public class UsersController : Controller
    {
        UserRepository repo = new UserRepository();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await repo.GetAll());
        }

        //GET: /Users/Register/
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new UserRegisterViewModel());
        }

        //POST: /Users/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterViewModel u)
        {
            if (ModelState.IsValid)
            {
                repo.Create(u.UserName, u.Password, u.Role);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        //GET: /Users/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(bool? ErrorMsg)
        {
            if (ErrorMsg != null)
                if ((bool)ErrorMsg)
                    ViewBag.msg = "To Access this page you need to be loged, please login.";

            return View(new UserLoginViewModel());
        }

        //POST: /Users/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel u)
        {

            if (ModelState.IsValid)
            {
                User user = repo.LogIn(u.UserName, u.Password);

                if (user != null)
                {
                    Session["isLoged"] = true;
                    Session["Role"] = user.Role;

                    return RedirectToAction("Index", "Users");
                }
            }

            return RedirectToAction("Index", "Home");
        }

        //GET: /Users/Logout
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                repo.Dispose();
            base.Dispose(disposing);
        }
    }
}