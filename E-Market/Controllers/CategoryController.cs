using E_Market.Core.Application.Interface.Services;
using E_Market.Core.Application.Services;
using E_Market.Core.Application.ViewModel.Anuncio;
using E_Market.Core.Application.ViewModel.Category;
using E_Market.Middlawares;
using Microsoft.AspNetCore.Mvc;

namespace E_Market.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        private readonly ValidateUserSession _validateUserSession;
        public CategoryController(ICategoryServices categoryServices, ValidateUserSession validateUserSession)
        {
            _categoryServices = categoryServices;
            _validateUserSession = validateUserSession;
        }


        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _categoryServices.GetAllViewModel());
        }
        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            CategorySaveViewModel vm = new();
            return View("SaveCategory", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategorySaveViewModel vm)
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }
            await _categoryServices.Add(vm);
            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View("SaveCategory", await _categoryServices.GetByIdSaveViewModel(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategorySaveViewModel vm)
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }
            await _categoryServices.Update(vm);
            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View("Delete", await _categoryServices.GetByIdSaveViewModel(id));
        }
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.hasUser())
            {
                return RedirectToRoute(new {controller="User", action= "Index"});
            }
            await _categoryServices.Delete(id);
            return RedirectToRoute(new {controller="Category", action= "Index"});
        }
    }
}
