using E_Market.Core.Application.ViewModel.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interface.Services
{
    public interface IImagenServices:IGeneryServices<SaveImagenViewModel,ImagenViewModel>
    {
        Task<List<ImagenViewModel>> GetAllImgForAnuncio(int idAnuncio);
    }
}
