using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class InvoiceValidator: AbstractValidator<InvoiceAddDto>
    {
        public InvoiceValidator()
        {
            RuleFor(i => i.InvoiceId).NotEmpty();
            RuleFor(i => i.InvoiceNo).NotEmpty();
            RuleFor(i => i.Products).NotEmpty();
            RuleFor(i => i.CustomerId).NotEmpty();
            RuleFor(i => i.Currency).NotEmpty();
            RuleFor(i => i.CreatedDate).NotEmpty();
            RuleFor(i => i.CreatedBy).NotEmpty(); 
            RuleFor(i => i.Address).NotEmpty();
        }
    }
}
