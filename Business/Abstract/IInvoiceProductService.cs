using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IInvoiceProductService
    {
        void Add(InvoiceProduct invoiceProduct);
        void Delete(int invoiceProductId);
        void Delete(InvoiceProduct invoiceProduct);
        void DeleteAllByInvoiceId(int invoiceId);
        IDataResult<IList<InvoiceProduct>> GetAllByInvoiceId(int invoiceId);
    }
}
