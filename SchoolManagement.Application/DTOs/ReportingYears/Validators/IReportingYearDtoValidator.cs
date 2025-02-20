using FluentValidation;
using SchoolManagement.Application.DTOs.ReportingYear;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ReportingYear.Validators
{
    public class IReportingYearDtoValidator : AbstractValidator<IReportingYearDto>
    {
        public IReportingYearDtoValidator()
        {
            RuleFor(p => p.Year)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

        
        }
    }

}
