using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.ViewModel.Category;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModel.Anuncio
{
    public class SaveAnuncioViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }
        public List<string>? Imagen { get; set; }
        [Required(ErrorMessage = "El campo Descripcion es requerido")]
        public string Descripcion { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "El campo Precio es requerido")]
        public double Precio { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "El campo CategoryId es requerido")]
        public int CategoryId { get; set; }
        public ICollection<CategoryViewModel> ?Categories { get; set; }

        [DataType(DataType.Upload)]
        public List<IFormFile>? File { get; set; }
    }
}
