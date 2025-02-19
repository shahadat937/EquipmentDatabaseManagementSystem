using FluentValidation;


namespace SchoolManagement.Application.DTOs.YearSetup.Validators
{
    public class UpdateYearSetupDtoValidator : AbstractValidator<YearSetupDto>
    {
        public UpdateYearSetupDtoValidator()
        {
            Include(new IYearSetupDtoValidator());

            RuleFor(p => p.Year).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}