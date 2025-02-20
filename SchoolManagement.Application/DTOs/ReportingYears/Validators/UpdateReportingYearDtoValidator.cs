using FluentValidation;
using SchoolManagement.Application.DTOs.ReportingYear;
using SchoolManagement.Application.DTOs.ReportingYear.Validators;


namespace SchoolManagement.Application.DTOs.ReporingYear.Validators
{
    public class UpdateReportingYearDtoValidator : AbstractValidator<ReportingYearDto>
    {
        public UpdateReportingYearDtoValidator()
        {
            Include(new IReportingYearDtoValidator());

            RuleFor(p => p.Year).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}