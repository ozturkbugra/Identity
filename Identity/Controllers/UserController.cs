using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
