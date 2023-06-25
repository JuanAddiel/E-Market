using E_Market.Core.Application.Interface.Repositories;
using E_Market.Core.Application.Interface.Services;
using E_Market.Core.Application.ViewModel.Category;
using E_Market.Core.Domain.Entites;
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
        public CategoryServices(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Add(CategorySaveViewModel vm)
        {
            Category ca = new();
            ca.Id = vm.Id;
            ca.Description = vm.Descripcion;
            ca.Nombre = vm.Nombre;
            await _categoryRepository.Add(ca);
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
            Category ca = new();
            ca.Id = vm.Id;
            ca.Description = vm.Descripcion;
            ca.Nombre = vm.Nombre;
            await _categoryRepository.Update(ca);
        }
    }
}
