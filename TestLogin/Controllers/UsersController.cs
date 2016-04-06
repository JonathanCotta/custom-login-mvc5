using System.Threading.Tasks;
using System.Web.Mvc;
using TestLogin.Models;
using TestLogin.Models.Repository;


namespace TestLogin.Controllers
{
    public class UsersController : Controller
    {
        UserRepository repo = new UserRepository(new Models.DataBase());        

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await repo.GetAll());
        }


        //GET: /Users/Register/
        public ActionResult Register()
        {
            return View(new User());
        }

        //POST: /Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string UserName , string Password , bool Role)
        {
                                
            repo.Create(UserName , Password , Role);            
            return RedirectToAction("Index","Home");
            
        }
              
        //POST: /Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string UserName, string Password)
        {
            User user = repo.LogIn(UserName, Password);

            if (user != null)
            {
                Session["isLoged"] = true;
                Session["UserName"] = user.UserName;
                Session["Role"] = user.Role;

                return RedirectToAction("Index", "Users");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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