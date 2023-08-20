using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IInvoiceService
    {
        IResult Add(InvoiceAddDto invoiceDetail);
        IResult Update(InvoiceAddDto invoiceDetail);
        IResult Delete(int invoiceId);
        IDataResult<List<InvoiceListDto>> GetAllList();
        IDataResult<InvoiceDetailDto> GetAllDetailsByInvoiceId(int invoiceId);
    }
}
