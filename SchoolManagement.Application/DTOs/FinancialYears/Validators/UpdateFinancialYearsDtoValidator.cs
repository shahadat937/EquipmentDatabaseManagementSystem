using FluentValidation;


namespace SchoolManagement.Application.DTOs.FinancialYears.Validators
{
    public class UpdateFinancialYearsDtoValidator : AbstractValidator<IFinancialYearsDto>
    {
        public UpdateFinancialYearsDtoValidator()
        {
            Include(new IFinancialYearsValidator());

            RuleFor(p => p.FinancialYearName).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}