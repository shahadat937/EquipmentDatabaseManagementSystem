using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReportType.Validators
{
    public class UpdateReportTypeDtoValidator : AbstractValidator<ReportTypeDto>
    {
        public UpdateReportTypeDtoValidator() 
        {
            Include(new IReportTypeDtoValidator());

            RuleFor(b => b.ReportTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
