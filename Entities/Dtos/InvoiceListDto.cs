using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class InvoiceListDto: IDto
    {
        public int InvoiceId { get; set; }
        public int InvoiceNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerName { get; set; }
    }
}
