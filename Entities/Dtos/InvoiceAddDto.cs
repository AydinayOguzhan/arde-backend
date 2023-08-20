using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class InvoiceAddDto: IDto
    {
        public int InvoiceId { get; set; }
        public int InvoiceNo { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Currency { get; set; }
        public int CreatedBy { get; set; }
        public int CustomerId { get; set; }
        public List<InvoiceProductDetailsDto> Products { get; set; }
    }
}
