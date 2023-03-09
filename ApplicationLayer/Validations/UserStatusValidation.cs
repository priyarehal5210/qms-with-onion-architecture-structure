using ApplicationLayer.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validations
{
    public class UserStatusValidation:AbstractValidator<UserSuccessDto>
    {
        public UserStatusValidation()
        {
            RuleFor(u=>u.assignTasksId).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("assignTaskId can't be empty");
            RuleFor(u => u.success).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("success can't be empty");
            RuleFor(u => u.hours).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("hours can't be empty");
            RuleFor(u => u.date).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("date can't be empty");
        }
    }
}
