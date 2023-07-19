using E_Market.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Domain.Entites
{
    public class Imagen : AuditableBaseEntity
    {
        public string ImageUrl { get; set; }
        public int idAnuncio { get; set; }
        public Anuncio anuncio { get; set; }
    }
}
