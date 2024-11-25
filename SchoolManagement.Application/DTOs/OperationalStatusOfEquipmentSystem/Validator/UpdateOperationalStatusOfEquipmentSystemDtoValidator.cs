using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.OperationalStatusOfEquipmentSystem.Validator
{
    public class UpdateOperationalStatusOfEquipmentSystemDtoValidator : AbstractValidator<CreateOperationalStatusOfEquipmentSystemDto>
    {
        public UpdateOperationalStatusOfEquipmentSystemDtoValidator()
        {
            Include (new IOperationalStatusOfEquipmentSystemDtoValidator());
            RuleFor(b => b.OperationalStatusOfEquipmentSystemId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
