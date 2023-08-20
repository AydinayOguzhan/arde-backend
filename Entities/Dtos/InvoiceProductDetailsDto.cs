using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class InvoiceProductDetailsDto: IDto
    {
        public int InvoiceProductId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double LineTotal { get; set; }
    }
}
