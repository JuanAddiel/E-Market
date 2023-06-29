using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Domain.Common
{
    public class AuditableBaseEntity
    {
        public virtual int Id { get; set; }
        public string Nombre { get; set; }
        public  DateTime CreatedDate { get; set; }
        public  DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
