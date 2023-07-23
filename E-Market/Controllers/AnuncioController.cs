using E_Market.Core.Application.Interface.Services;
using E_Market.Core.Application.ViewModel.Anuncio;
using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.ViewModel.Image;
using E_Market.Core.Domain.Entites;
using E_Market.Middlawares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Market.Controllers
{
    public class AnuncioController : Controller
    {
        private readonly IAnuncioServices _anuncioServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IImagenServices _imagenServices;

        public AnuncioController(IImagenServices imagenServices,IAnuncioServices anuncioServices, ICategoryServices categoryServices, ValidateUserSession validateUserSession)
        {
            _anuncioServices = anuncioServices;
            _categoryServices = categoryServices;
            _validateUserSession = validateUserSession;
            _imagenServices = imagenServices;
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

            SaveAnuncioViewModel anunciovm = await _anuncioServices.Add(vm);
            if (anunciovm != null && anunciovm.Id != 0)
            {
                List<string> imagePaths = UploadFile.UploadFiles(vm.File, anunciovm.Id);
                foreach (var imgPath in imagePaths)
                {
                    var imagen = new Imagen
                    {
                        Nombre = Path.GetFileName(imgPath),
                        ImageUrl = imgPath,
                        idAnuncio = anunciovm.Id
                    };
                    SaveImagenViewModel sv = new();
                    sv.Id = imagen.Id;
                    sv.ImageUrl = imagen.ImageUrl;
                    sv.Nombre = imagen.Nombre;
                    sv.IdAnuncio = imagen.idAnuncio;

                    await _imagenServices.Add(sv);
                }
                await _anuncioServices.Update(anunciovm);
            }
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

            SaveAnuncioViewModel anunciovm = await _anuncioServices.GetByIdSaveViewModel(vm.Id);

            List<string> imagePaths = UploadFile.UploadFiles(vm.File, anunciovm.Id);
            foreach (var imgPath in imagePaths)
            {
                var imagen = new Imagen
                {
                    Nombre = Path.GetFileName(imgPath),
                    ImageUrl = imgPath,
                    idAnuncio = anunciovm.Id
                };
                SaveImagenViewModel sv = new();
                sv.Id = imagen.Id;
                sv.ImageUrl = imagen.ImageUrl;
                sv.Nombre = imagen.Nombre;
                sv.IdAnuncio = imagen.idAnuncio;

                await _imagenServices.Add(sv);
            }

            await _anuncioServices.Update(anunciovm);

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
            string basePath = $"/Image/Anuncio/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo item in directoryInfo.GetFiles())
                {
                    item.Delete();
                }
                foreach (DirectoryInfo item in directoryInfo.GetDirectories())
                {
                    item.Delete(true);
                }
            }
            return RedirectToRoute(new { controller = "Anuncio", action = "Index" });
        }

    }
}
