
using FluentValidation;

namespace SchoolManagement.Application.DTOs.FcLc.Validators
{
    public class IFcLcDtoValidator : AbstractValidator<IFcLcDto>
    {
        public IFcLcDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
