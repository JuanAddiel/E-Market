using E_Market.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Domain.Entites
{
    public class Anuncio:AuditableBaseEntity
    {
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int UsuarioId { get; set; }
        public User Usuario { get; set; }

    }
}
