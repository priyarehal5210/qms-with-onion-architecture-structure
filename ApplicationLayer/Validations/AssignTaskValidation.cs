using ApplicationLayer.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validations
{
    public class AssignTaskValidation:AbstractValidator<AssignTasksDto>
    {
        public AssignTaskValidation()
        {
            RuleFor(u => u.registeredUsersId).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("registeredUserId can't be empty");
            RuleFor(u => u.tasksId).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("taskId can't be empty");
        }
    }
}
