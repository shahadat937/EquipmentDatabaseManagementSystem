using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.CodeValues.Validators
{
    public class CreateCodeValueDtoValidator:AbstractValidator<CreateCodeValueDto>
    {
        public CreateCodeValueDtoValidator() 
        { 
            Include(new ICodeValueDtoValidator());
        }
    }
}
