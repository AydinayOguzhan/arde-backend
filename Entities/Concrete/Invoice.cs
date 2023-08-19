using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Invoice: IEntity
    {
        public int Id { get; set; }
        public int CustomerNo { get; set; }
        public int CurrencyId { get; set; }
        public int CreatedBy { get; set; }
        public int InvoiceNo { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
