using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReportingMonths.Validators
{
    public class UpdateReportingMonthDtoValidator : AbstractValidator<ReportingMonthDto>
    {
        public UpdateReportingMonthDtoValidator() 
        {
            Include(new IReportingMonthDtoValidator());

            RuleFor(b => b.ReportingMonthId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
