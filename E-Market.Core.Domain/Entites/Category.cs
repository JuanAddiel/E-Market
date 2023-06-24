using E_Market.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Domain.Entites
{
    public class Category:AuditableBaseEntity
    {
        public string Description { get; set; }
        public virtual ICollection<Anuncio> Anuncio { get; set; }
    }
}
