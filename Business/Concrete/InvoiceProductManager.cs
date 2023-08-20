using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class InvoiceProductManager : IInvoiceProductService
    {
        private IInvoiceProductDal _invoiceProductDal;

        public InvoiceProductManager(IInvoiceProductDal invoiceProductDal)
        {
            _invoiceProductDal = invoiceProductDal;
        }

        public void Add(InvoiceProduct invoiceProduct)
        {
            _invoiceProductDal.Add(invoiceProduct);
        }

        public void Delete(int invoiceProductId)
        {
            var invoiceProduct = _invoiceProductDal.Get(i => i.Id == invoiceProductId);
            _invoiceProductDal.Delete(invoiceProduct);
        }

        public void Delete(InvoiceProduct invoiceProduct)
        {
            _invoiceProductDal.Delete(invoiceProduct);
        }

        public void DeleteAllByInvoiceId(int invoiceId)
        {
            var result = _invoiceProductDal.GetList(i => i.InvoiceId == invoiceId);
            foreach (var item in result)
            {
                _invoiceProductDal.Delete(item);
            }
        }

        public IDataResult<IList<InvoiceProduct>> GetAllByInvoiceId(int invoiceId)
        {
            var invoiceProduct = _invoiceProductDal.GetList(i => i.InvoiceId == invoiceId);
            if (invoiceProduct == null)
            {
                return new ErrorDataResult<IList<InvoiceProduct>>(Messages.NoProductsFound);
            }
            return new SuccessDataResult<IList<InvoiceProduct>>(invoiceProduct);
        }
    }
}
