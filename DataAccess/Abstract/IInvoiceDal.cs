﻿using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IInvoiceDal: IEntityRepository<Invoice>
    {
        InvoiceDetailDto GetAllDetailsByInvoiceId(int invoiceId);
        List<InvoiceListDto> GetAllList();
    }
}
