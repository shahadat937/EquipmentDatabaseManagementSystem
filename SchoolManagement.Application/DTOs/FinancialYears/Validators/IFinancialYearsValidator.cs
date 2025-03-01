using FluentValidation;
using SchoolManagement.Application.DTOs.ReportingYear;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FinancialYears.Validators
{
    public class IFinancialYearsValidator : AbstractValidator<IFinancialYearsDto>
    {
        public IFinancialYearsValidator()
        {
            RuleFor(p => p.FinancialYearName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

        
        }
    }

}
