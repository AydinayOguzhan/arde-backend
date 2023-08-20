using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class RegisterValidator: AbstractValidator<UserForRegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(u => u.Email).EmailAddress().NotEmpty();
            RuleFor(u => u.Password).NotEmpty().MinimumLength(6);
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
        }
    }
}
