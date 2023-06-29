using E_Market.Core.Application.Interface.Services;
using E_Market.Core.Application.ViewModel.User;
using E_Market.Middlawares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_Market.Core.Application.Helpers;

namespace E_Market.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _usersService;
        private readonly ValidateUserSession _validateUserSession;
        public UserController(IUserServices usersService, ValidateUserSession validateUserSession)
        {
            _usersService = usersService;
            _validateUserSession = validateUserSession;
        }
        public IActionResult Index()
        {
            if (_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginvm)
        {
            if (_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View(loginvm);
            }
            UserViewModel uservm = await _usersService.Login(loginvm);
            if (uservm != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", uservm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Usuario o contraseña incorrecta");
            }
            return View(loginvm);
        }
        public IActionResult Register()
        {
            if (_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(new SaveUserViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel Uservm)
        {
            if (_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View(Uservm);
            }
            if (await _usersService.GetNombre(Uservm) == true)
            {
                ModelState.AddModelError("userValidation", "Nombre de usuario creado");
                return View(Uservm);
            }
            else
            {
                await _usersService.Add(Uservm);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
