using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModel.Anuncio
{
    public class AnuncioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int CategoryId { get; set; }
        public int UsuarioId { get; set; }
    }
}
