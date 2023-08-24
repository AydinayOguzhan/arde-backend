using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class InvoiceProductValidator: AbstractValidator<InvoiceProduct>
    {
        public InvoiceProductValidator()
        {
            RuleFor(i=>i.Id).NotEmpty();
            RuleFor(i => i.InvoiceId).NotEmpty();
            RuleFor(i => i.LineTotal).NotEmpty().GreaterThan(0);
            RuleFor(i => i.ProductId).NotEmpty();
            RuleFor(i => i.Quantity).NotEmpty().GreaterThan(0);
        }
    }
}
