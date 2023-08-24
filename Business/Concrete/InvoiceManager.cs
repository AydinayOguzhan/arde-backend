using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class InvoiceManager: IInvoiceService
    {
        private IInvoiceDal _invoiceDal;
        private ICurrencyService _currencyService;
        private IInvoiceProductService _invoiceProductService;

        public InvoiceManager(IInvoiceDal invoiceDal, ICurrencyService currencyService, IInvoiceProductService invoiceProductService)
        {
            _invoiceDal = invoiceDal;
            _currencyService = currencyService;
            _invoiceProductService = invoiceProductService;
        }

        [ValidationAspect(typeof(InvoiceValidator))]
        public IResult Add(InvoiceAddDto invoiceDetail)
        {
            //var currency = _currencyService.GetByName(invoiceDetail.Currency);
            Invoice invoice = new Invoice
            {
                InvoiceNo = invoiceDetail.InvoiceNo,
                Address = invoiceDetail.Address,
                CreatedDate = invoiceDetail.CreatedDate,
                CurrencyId = Convert.ToInt32(invoiceDetail.Currency),
                CreatedBy = invoiceDetail.CreatedBy,
                CustomerNo = invoiceDetail.CustomerId
            };
            _invoiceDal.Add(invoice);

            var invoiceResult = _invoiceDal.Get(i => i.InvoiceNo == invoice.InvoiceNo);
            foreach (var product in invoiceDetail.Products)
            {
                var invoiceProduct = new InvoiceProduct
                {
                    LineTotal = product.LineTotal,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    InvoiceId = invoiceResult.Id
                };
                _invoiceProductService.Add(invoiceProduct);
            }
            return new SuccessResult(Messages.Successful);
        }

        public IResult Delete(int invoiceId)
        {
            var invoiceResult = _invoiceDal.Get(i => i.Id == invoiceId);
            if (invoiceResult == null)
            {
                return new ErrorResult(Messages.InvoiceNotFound);
            }
            _invoiceDal.Delete(invoiceResult);
            
            var invoiceProducts = _invoiceProductService.GetAllByInvoiceId(invoiceId);
            foreach (var invoiceProduct in invoiceProducts.Data)
            {
                _invoiceProductService.Delete(invoiceProduct);
            }
            return new SuccessResult(Messages.Successful);
        }

        public IDataResult<InvoiceDetailDto> GetAllDetailsByInvoiceId(int invoiceId)
        {
            return new SuccessDataResult<InvoiceDetailDto>(_invoiceDal.GetAllDetailsByInvoiceId(invoiceId));
        }

        public IDataResult<List<InvoiceListDto>> GetAllList()
        {
            return new SuccessDataResult<List<InvoiceListDto>>(_invoiceDal.GetAllList()); ;
        }

        public IResult Update(InvoiceAddDto invoiceDetail)
        {
            var currency = _currencyService.GetByName(invoiceDetail.Currency);
            Invoice invoice = new Invoice
            {
                Id = invoiceDetail.InvoiceId,
                InvoiceNo = invoiceDetail.InvoiceNo,
                Address = invoiceDetail.Address,
                CreatedDate = invoiceDetail.CreatedDate,
                CurrencyId = currency.Data.Id,
                CreatedBy = invoiceDetail.CreatedBy,
                CustomerNo = invoiceDetail.CustomerId
            };
            _invoiceDal.Update(invoice);

            _invoiceProductService.DeleteAllByInvoiceId(invoiceDetail.InvoiceId);

            foreach (var product in invoiceDetail.Products)
            {
                var invoiceProduct = new InvoiceProduct
                {
                    LineTotal = product.LineTotal,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    InvoiceId = invoiceDetail.InvoiceId
                };
                _invoiceProductService.Add(invoiceProduct);
            }
            return new SuccessResult(Messages.Successful);
        }
    }
}
