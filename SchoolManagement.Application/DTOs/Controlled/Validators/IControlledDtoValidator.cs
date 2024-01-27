
using FluentValidation;

namespace SchoolManagement.Application.DTOs.Controlled.Validators
{
    public class IControlledDtoValidator : AbstractValidator<IControlledDto>
    {
        public IControlledDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
