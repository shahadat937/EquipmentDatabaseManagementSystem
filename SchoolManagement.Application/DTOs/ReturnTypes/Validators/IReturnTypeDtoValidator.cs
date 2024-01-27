
using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReturnTypes.Validators
{
    public class IReturnTypeDtoValidator : AbstractValidator<IReturnTypeDto>
    {
        public IReturnTypeDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
