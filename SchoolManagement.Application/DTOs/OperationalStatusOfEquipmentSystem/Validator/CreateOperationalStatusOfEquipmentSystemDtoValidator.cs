using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.OperationalStatusOfEquipmentSystem.Validator
{
    public class CreateOperationalStatusOfEquipmentSystemDtoValidator : AbstractValidator<CreateOperationalStatusOfEquipmentSystemDto>
    {
        public CreateOperationalStatusOfEquipmentSystemDtoValidator()
        {
            Include(new IOperationalStatusOfEquipmentSystemDtoValidator());
        }
    }
}
