using E_Market.Core.Application.ViewModel.Category;
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
        [Required(ErrorMessage = "El campo Imagen es requerido")]
        [DataType(DataType.Text)]
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo Precio es requerido")]
        [DataType(DataType.Currency)]
        public double Precio { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "El campo CategoryId es requerido")]
        public int CategoryId { get; set; }
        public ICollection<CategoryViewModel> ?Categories { get; set; }
    }
}
