using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interface.Repositories;
using E_Market.Core.Application.Interface.Services;
using E_Market.Core.Application.ViewModel.Anuncio;
using E_Market.Core.Application.ViewModel.User;
using E_Market.Core.Domain.Entites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Services
{
    public class AnuncioServices : IAnuncioServices
    {
        private readonly IAnuncioRepository _anuncioRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;
        private readonly IImagenServices _imagenServices;
        public AnuncioServices(IAnuncioRepository anuncioRepository, IHttpContextAccessor httpContextAccessor, IImagenServices imagenServices)
        {
            _anuncioRepository = anuncioRepository;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _imagenServices = imagenServices;
        }

        public async Task<SaveAnuncioViewModel> Add(SaveAnuncioViewModel vm)
        {
            Anuncio anuncio = new();
            anuncio.Id = vm.Id;
            anuncio.Nombre = vm.Nombre;
            anuncio.Precio = vm.Precio;
            anuncio.Descripcion = vm.Descripcion;
            anuncio.CategoryId = vm.CategoryId;
            anuncio.UsuarioId = _userViewModel.Id;

            anuncio = await _anuncioRepository.Add(anuncio);

            SaveAnuncioViewModel anunciovm = new();
            anunciovm.Id = anuncio.Id;
            anunciovm.Nombre = anuncio.Nombre;
            anunciovm.Precio = anuncio.Precio;
            anunciovm.Descripcion = anuncio.Descripcion;
            anunciovm.CategoryId = anuncio.CategoryId;
            return anunciovm;
        }

        public async Task Delete(int id)
        {
            var anuncio = await _anuncioRepository.GetByIdAsync(id);
            await _anuncioRepository.Delete(anuncio);
        }

        public async Task<List<AnuncioViewModel>> GetAllViewModel()
        {
            var anuncio = await _anuncioRepository.GetAllWithInclude(new List<string> { "Category", "Imagen"});

            return anuncio.Where(a => a.UsuarioId == _userViewModel.Id).Select( a => new AnuncioViewModel
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Descripcion = a.Descripcion,
                CategoryId = a.CategoryId,
                Imagen = a.Imagen,
                Precio = a.Precio,
                CategoryName = a.Category.Nombre

            }).ToList();
        }

        public async Task<SaveAnuncioViewModel> GetByIdSaveViewModel(int id)
        {
            var anuncio = await _anuncioRepository.GetByIdAsync(id);
            SaveAnuncioViewModel vm = new();
            vm.Id = anuncio.Id;
            vm.Nombre = anuncio.Nombre;
            vm.Precio = anuncio.Precio;
            vm.Descripcion = anuncio.Descripcion;
            vm.CategoryId = anuncio.CategoryId;

            return vm;
        }

        public async Task Update(SaveAnuncioViewModel vm)
        {
            Anuncio anuncio = await _anuncioRepository.GetByIdAsync(vm.Id);
            anuncio.Id = vm.Id;
            anuncio.Nombre = vm.Nombre;
            anuncio.Precio = vm.Precio;
            anuncio.Descripcion = vm.Descripcion;
            anuncio.CategoryId = vm.CategoryId;

            await _anuncioRepository.Update(anuncio);
        }
        public async Task<List<AnuncioViewModel>> GetAllViewModelWithFilters(FilterAnuncioViewModel filters)
        {
            var anuncioList = await _anuncioRepository.GetAllWithInclude(new List<string> { "Category", "Imagen" });
            var list = anuncioList.Where(Anuncio => Anuncio.UsuarioId != _userViewModel.Id).Select(a => new AnuncioViewModel
            {
                Nombre = a.Nombre,
                Id = a.Id,
                CategoryId = a.Category.Id,
                Imagen = a.Imagen,
                Precio = a.Precio,
                Descripcion = a.Descripcion,
                CategoryName = a.Category.Nombre

            }).ToList();
            if (filters.CategoryId != null)
            {
                list = list.Where(a => a.CategoryId == filters.CategoryId.Value).ToList();
            }
            return list;
        }
    }
}
