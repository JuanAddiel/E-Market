using E_Market.Core.Application.ViewModel.Anuncio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interface.Services
{
    public interface IAnuncioServices:IGeneryServices<SaveAnuncioViewModel, AnuncioViewModel>
    {
    }
}
