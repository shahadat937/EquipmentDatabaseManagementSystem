
using FluentValidation;

namespace SchoolManagement.Application.DTOs.ActionTaken.Validators
{
    public class IActionTakenDtoValidator : AbstractValidator<IActionTakenDto>
    {
        public IActionTakenDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
