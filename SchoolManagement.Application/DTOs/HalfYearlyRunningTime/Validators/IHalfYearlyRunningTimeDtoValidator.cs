
using FluentValidation;

namespace SchoolManagement.Application.DTOs.HalfYearlyRunningTime.Validators
{
    public class IHalfYearlyRunningTimeDtoValidator : AbstractValidator<IHalfYearlyRunningTimeDto>
    {
        public IHalfYearlyRunningTimeDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
