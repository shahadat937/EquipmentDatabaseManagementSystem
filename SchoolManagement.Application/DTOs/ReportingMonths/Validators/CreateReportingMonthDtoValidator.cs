using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReportingMonths.Validators
{
    public class CreateReportingMonthDtoValidator : AbstractValidator<CreateReportingMonthDto>
    {
        public CreateReportingMonthDtoValidator()
        {
            Include(new IReportingMonthDtoValidator());
        }
    }
}
 