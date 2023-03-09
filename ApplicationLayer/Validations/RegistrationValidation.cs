using ApplicationLayer.Dtos;
using DomainLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Validations
{
    public class RegistrationValidation:AbstractValidator<RegisteredUserDto>
    {
        public RegistrationValidation()
        {
            RuleFor(u=>u.userName).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("user name can't be empty").Length(6,10);
            RuleFor(u => u.email).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("email can't be empty").EmailAddress();
            RuleFor(u => u.password).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("password can't be empty").Length(6, 12);
            RuleFor(u => u.confirmPassword).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("confirm password can't be empty").Equal(u => u.password).WithMessage("must be same to password");
        }
    }
}
