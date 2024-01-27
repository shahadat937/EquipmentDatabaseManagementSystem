
using FluentValidation;

namespace SchoolManagement.Application.DTOs.DailyWorkState.Validators
{
    public class IDailyWorkStateDtoValidator : AbstractValidator<IDailyWorkStateDto>
    {
        public IDailyWorkStateDtoValidator()
        {
            RuleFor(b => b.Subject)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
