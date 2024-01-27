
using FluentValidation;

namespace SchoolManagement.Application.DTOs.LetterTypes.Validators
{
    public class ILetterTypeDtoValidator : AbstractValidator<ILetterTypeDto>
    {
        public ILetterTypeDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
