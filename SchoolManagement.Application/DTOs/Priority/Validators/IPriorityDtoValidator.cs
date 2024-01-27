
using FluentValidation;

namespace SchoolManagement.Application.DTOs.Priority.Validators
{
    public class IPriorityDtoValidator : AbstractValidator<IPriorityDto>
    {
        public IPriorityDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
