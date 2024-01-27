
using FluentValidation;

namespace SchoolManagement.Application.DTOs.Tec.Validators
{
    public class ITecDtoValidator : AbstractValidator<ITecDto>
    {
        public ITecDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
