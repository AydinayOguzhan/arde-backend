using Core.Entities;
using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class InvoiceDetailDto: IDto
    {
        public int InvoiceId { get; set; }
        public int InvoiceNo { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Currency { get; set; }

        public User CreatedBy { get; set; }
        public Customer Customer { get; set; }
        public List<InvoiceProductDetailsDto> Products { get; set; }
    }
}
