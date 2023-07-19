using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interface.Repositories;
using E_Market.Core.Application.Interface.Services;
using E_Market.Core.Application.ViewModel.Anuncio;
using E_Market.Core.Application.ViewModel.Category;
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
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        public CategoryServices(ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
           userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<CategorySaveViewModel> Add(CategorySaveViewModel vm)
        {
            Category ca = new();
            ca.Id = vm.Id;
            ca.Description = vm.Descripcion;
            ca.Nombre = vm.Nombre;
            ca = await _categoryRepository.Add(ca);

            CategorySaveViewModel categoryvm = new();
            categoryvm.Id = ca.Id;
            categoryvm.Nombre = ca.Nombre;
            categoryvm.Descripcion = ca.Description;

            return categoryvm;
        }

        public async Task Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.Delete(category);
        }

        public async Task<List<CategoryViewModel>> GetAllViewModel()
        {
            var category = await _categoryRepository.GetAllWithInclude(new List<string> { "Anuncio"});
            return category.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Description,
                AnuncioQuantity = c.Anuncio.Where(a => a.UsuarioId == userViewModel.Id).Count()
            }).ToList();
        }

        public async Task<CategorySaveViewModel> GetByIdSaveViewModel(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            CategorySaveViewModel vm = new();
            vm.Id = category.Id;
            vm.Nombre = category.Nombre;
            vm.Descripcion = category.Description;
            return vm;
        }

        public async Task Update(CategorySaveViewModel vm)
        {
            Category ca = await _categoryRepository.GetByIdAsync(vm.Id);
            ca.Id = vm.Id;
            ca.Description = vm.Descripcion;
            ca.Nombre = vm.Nombre;
            await _categoryRepository.Update(ca);
        }
    }
}
