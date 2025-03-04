using FluentValidation;


namespace SchoolManagement.Application.DTOs.FinancialYears.Validators
{
    public class UpdateFinancialYearDtoValidator : AbstractValidator<IFinancialYearDto>
    {
        public UpdateFinancialYearDtoValidator()
        {
            Include(new IFinancialYearValidator());

            RuleFor(p => p.FinancialYearName).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}