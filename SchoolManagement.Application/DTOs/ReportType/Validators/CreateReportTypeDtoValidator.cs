using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReportType.Validators
{
    public class CreateReportTypeDtoValidator : AbstractValidator<CreateReportTypeDto>
    {
        public CreateReportTypeDtoValidator()
        {
            Include(new IReportTypeDtoValidator());
        }
    }
}
 