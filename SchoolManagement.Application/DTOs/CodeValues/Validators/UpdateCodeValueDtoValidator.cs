using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.CodeValues.Validators
{
    public class UpdateCodeValueDtoValidator : AbstractValidator<CodeValueDto>
    {
        public UpdateCodeValueDtoValidator()
        {
            Include(new ICodeValueDtoValidator()); 

            RuleFor(p => p.CodeValueId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
