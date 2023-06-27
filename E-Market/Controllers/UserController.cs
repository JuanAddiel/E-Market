using Microsoft.AspNetCore.Mvc;

namespace E_Market.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
