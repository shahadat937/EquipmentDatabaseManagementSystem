
using FluentValidation;

namespace SchoolManagement.Application.DTOs.RunningTimes.Validators
{
    public class IRunningTimeDtoValidator : AbstractValidator<IRunningTimeDto>
    {
        public IRunningTimeDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
