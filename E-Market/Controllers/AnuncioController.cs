using E_Market.Core.Application.Interface.Services;
using E_Market.Core.Application.ViewModel.Anuncio;
using E_Market.Middlawares;
using Microsoft.AspNetCore.Mvc;

namespace E_Market.Controllers
{
    public class AnuncioController : Controller
    {
        private readonly IAnuncioServices _anuncioServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ValidateUserSession _validateUserSession;

        public AnuncioController(IAnuncioServices anuncioServices, ICategoryServices categoryServices, ValidateUserSession validateUserSession)
        {
            _anuncioServices = anuncioServices;
            _categoryServices = categoryServices;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _anuncioServices.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            SaveAnuncioViewModel vm = new();
            vm.Categories = await _categoryServices.GetAllViewModel();
            return View("SaveAnuncio", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveAnuncioViewModel vm)
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryServices.GetAllViewModel();
                return View("SaveAnuncio", vm);
            }
            await _anuncioServices.Add(vm);
            return RedirectToRoute(new { controller = "Anuncio", action = "Index" });
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            SaveAnuncioViewModel vm = await _anuncioServices.GetByIdSaveViewModel(id);
            vm.Categories = await _categoryServices.GetAllViewModel();
            return View("SaveAnuncio", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SaveAnuncioViewModel vm)
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryServices.GetAllViewModel();
                return View("SaveAnuncio", vm);
            }
            await _anuncioServices.Update(vm);
            return RedirectToRoute(new { controller = "Anuncio", action = "Index" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View("Delete", await _anuncioServices.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            await _anuncioServices.Delete(id);
            return RedirectToRoute(new { controller = "Anuncio", action = "Index" });
        }
    }
}
