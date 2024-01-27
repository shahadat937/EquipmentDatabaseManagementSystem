
using FluentValidation;

namespace SchoolManagement.Application.DTOs.Sqns.Validators
{
    public class ISqnDtoValidator : AbstractValidator<ISqnDto>
    {
        public ISqnDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
