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
    public class TaskValidation:AbstractValidator<TasksDto>
    {
        public TaskValidation()
        {
            RuleFor(t => t.name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("task name can't be empty");
            RuleFor(t => t.description).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("desccription can't be empty");
        }
    }
}
