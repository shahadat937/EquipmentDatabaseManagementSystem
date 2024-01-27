
using FluentValidation;

namespace SchoolManagement.Application.DTOs.EqupmentNames.Validators
{
    public class IEqupmentNameDtoValidator : AbstractValidator<IEqupmentNameDto>
    {
        public IEqupmentNameDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
