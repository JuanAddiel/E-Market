using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModel.Image
{
    public class SaveImagenViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ImageUrl { get; set; }
        public int IdAnuncio { get; set; }
    }
}
