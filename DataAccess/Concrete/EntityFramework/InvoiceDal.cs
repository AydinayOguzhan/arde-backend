using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class InvoiceDal : EfEntityRepositoryBase<Invoice, EfContext>, IInvoiceDal
    {
        public InvoiceDetailDto GetAllDetailsByInvoiceId(int invoiceId)
        {
            using (var context = new EfContext())
            {
                var result = (from invoice in context.Invoices
                              join invoiceProducts in context.InvoiceProducts
                                 on invoice.Id equals invoiceProducts.InvoiceId
                              join product in context.Products
                                 on invoiceProducts.ProductId equals product.Id
                              join customer in context.Customers
                                 on invoice.CustomerNo equals customer.Id
                              join currency in context.Currencies
                                 on invoice.CurrencyId equals currency.Id
                              where invoice.Id == invoiceId
                              select new InvoiceDetailDto
                              {
                                  InvoiceId = invoice.Id,
                                  InvoiceNo = invoice.InvoiceNo,
                                  Address = invoice.Address,
                                  CreatedDate = invoice.CreatedDate,
                                  Currency = currency.Name,
                                  CreatedBy = context.Users.FirstOrDefault(u => u.Id == invoice.CreatedBy),
                                  Customer = context.Customers.FirstOrDefault(c => c.Id == invoice.CustomerNo),
                                  Products = (from invoiceProduct in context.InvoiceProducts
                                              where invoiceProduct.InvoiceId == invoice.Id
                                              join product in context.Products
                                                 on invoiceProduct.ProductId equals product.Id
                                              select new InvoiceProductDetailsDto
                                              {
                                                  InvoiceProductId = invoiceProduct.Id,
                                                  ProductId = product.Id,
                                                  Name = product.Name,
                                                  UnitPrice = product.UnitPrice,
                                                  Quantity = invoiceProduct.Quantity,
                                                  LineTotal = invoiceProduct.LineTotal
                                              }).ToList()
                              }).FirstOrDefault();
                return result;
            }
        }

        public List<InvoiceListDto> GetAllList()
        {
            using (var context = new EfContext())
            {
                var result = (from invoice in context.Invoices
                              join customer in context.Customers
                                on invoice.CustomerNo equals customer.Id
                              select new InvoiceListDto
                              {
                                  InvoiceId = invoice.Id,
                                  InvoiceNo = invoice.InvoiceNo,
                                  CreatedDate = invoice.CreatedDate,
                                  CustomerName = customer.CustomerName
                              }).ToList();
                return result;
            }
        }
    }
}
