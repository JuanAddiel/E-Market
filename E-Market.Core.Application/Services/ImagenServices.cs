using E_Market.Core.Application.Interface.Repositories;
using E_Market.Core.Application.Interface.Services;
using E_Market.Core.Application.ViewModel.Image;
using E_Market.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Services
{
    public class ImagenServices : IImagenServices
    {
        private readonly IImagenRepository _imagenRepository;
        public ImagenServices (IImagenRepository imagenRepository)
        {
            _imagenRepository = imagenRepository;
        }
        public async Task<SaveImagenViewModel> Add(SaveImagenViewModel vm)
        {
            Imagen img = new();
            img.Id = vm.Id;
            img.ImageUrl = vm.ImageUrl;
            img.idAnuncio = vm.IdAnuncio;
            img.Nombre = vm.Nombre;
            img = await _imagenRepository.Add(img);

            SaveImagenViewModel imgVm = new();
            imgVm.Id = img.Id;
            imgVm.ImageUrl = img.ImageUrl;
            imgVm.IdAnuncio = img.idAnuncio;
            imgVm.Nombre = img.Nombre;
            return imgVm;

        }

        public async Task<List<ImagenViewModel>> GetAllImgForAnuncio(int idAnuncio)
        {
            List<Imagen> imagen = await _imagenRepository.GetAll();
            List<ImagenViewModel> imagenViews = imagen
                .Where(i => i.idAnuncio == idAnuncio)
                .Select(i => new ImagenViewModel
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    Nombre = i.Nombre,
                    IdAnuncio = i.idAnuncio
                }).ToList();

            return imagenViews;
        }
        public async Task Delete(int id)
        {
            var imagen = await _imagenRepository.GetByIdAsync(id);  
            await _imagenRepository.Delete(imagen);
        }

        #region NoUtilizado


        public Task<List<ImagenViewModel>> GetAllViewModel()
        {
            throw new NotImplementedException();
        }

        public Task<SaveImagenViewModel> GetByIdSaveViewModel(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(SaveImagenViewModel vm)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
