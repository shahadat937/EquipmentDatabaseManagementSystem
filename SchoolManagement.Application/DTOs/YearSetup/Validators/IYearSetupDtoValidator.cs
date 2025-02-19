using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.YearSetup.Validators
{
    public class IYearSetupDtoValidator : AbstractValidator<IYearSetupDto>
    {
        public IYearSetupDtoValidator()
        {
            RuleFor(p => p.Year)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            //RuleFor(p => p.LoweredYearSetupName)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }

}
