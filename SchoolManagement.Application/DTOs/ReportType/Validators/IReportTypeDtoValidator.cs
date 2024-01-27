
using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReportType.Validators
{
    public class IReportTypeDtoValidator : AbstractValidator<IReportTypeDto>
    {
        public IReportTypeDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
