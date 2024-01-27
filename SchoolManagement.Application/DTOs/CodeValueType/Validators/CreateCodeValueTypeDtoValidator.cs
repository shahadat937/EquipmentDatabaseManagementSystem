using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.CodeValueType.Validators
{
    public class CreateCodeValueTypeDtoValidator : AbstractValidator<CreateCodeValueTypeDto>
    {
        public CreateCodeValueTypeDtoValidator()
        {
            Include(new ICodeValueTypeDtoValidator());
        }
    }
}
