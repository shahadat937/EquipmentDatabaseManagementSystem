using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.BaseSchoolNames.Validators
{
    public class CreateBaseSchoolNameDtoValidator:AbstractValidator<CreateBaseSchoolNameDto>
    {
        public CreateBaseSchoolNameDtoValidator() 
        { 
            Include(new IBaseSchoolNameDtoValidator()); 
        }
    }
}
