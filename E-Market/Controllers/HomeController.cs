using E_Market.Core.Application.Interface.Services;
using E_Market.Core.Application.ViewModel.Anuncio;
using E_Market.Middlawares;
using E_Market.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Market.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnuncioServices _anuncioServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ValidateUserSession _validateUserSession;

        public HomeController(IAnuncioServices anuncioServices, ICategoryServices categoryServices, ValidateUserSession validateUserSession)
        {
            _anuncioServices = anuncioServices;
            _categoryServices = categoryServices;
            _validateUserSession = validateUserSession;
        }

        //Investiga si hay usuario si no hay manda a la vista index, del controlador user
        public async Task<IActionResult> Index(FilterAnuncioViewModel vm)
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            ViewBag.Category = await _categoryServices.GetAllViewModel();
            return View(await _anuncioServices.GetAllViewModelWithFilters(vm));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}