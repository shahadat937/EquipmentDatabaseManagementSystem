using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Modules.Validators
{
    public class CreateModuleDtoValidator:AbstractValidator<CreateModuleDto>
    {
        public CreateModuleDtoValidator() 
        { 
            Include(new IModuleDtoValidator());
        }
    }
}
