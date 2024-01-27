using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.CodeValues.Validators
{
    public class ICodeValueDtoValidator : AbstractValidator<ICodeValueDto>
    {
        public ICodeValueDtoValidator()
        {
            RuleFor(p => p.Code) 
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.CodeValueTypeId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.TypeValue)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.DisplayCode)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
