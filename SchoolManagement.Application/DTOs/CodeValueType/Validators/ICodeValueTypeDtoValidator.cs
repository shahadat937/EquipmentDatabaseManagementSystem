using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.CodeValueType.Validators
{ 
    public class ICodeValueTypeDtoValidator : AbstractValidator<ICodeValueTypeDto>
    {
        public ICodeValueTypeDtoValidator()
        {
            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
