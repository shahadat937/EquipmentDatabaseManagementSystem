
using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReportingMonths.Validators
{
    public class IReportingMonthDtoValidator : AbstractValidator<IReportingMonthDto>
    {
        public IReportingMonthDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
