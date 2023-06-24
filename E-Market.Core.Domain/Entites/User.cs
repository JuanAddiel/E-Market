using E_Market.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Domain.Entites
{
    public class User:AuditableBaseEntity
    {
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string NombreUsuario { get; set; }
        public ICollection<Anuncio> Anuncios { get; set; }
    }
}
