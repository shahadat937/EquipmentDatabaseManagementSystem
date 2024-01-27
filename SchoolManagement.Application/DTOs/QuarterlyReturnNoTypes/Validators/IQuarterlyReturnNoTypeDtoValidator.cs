
using FluentValidation;

namespace SchoolManagement.Application.DTOs.QuarterlyReturnNoTypes.Validators
{
    public class IQuarterlyReturnNoTypeDtoValidator : AbstractValidator<IQuarterlyReturnNoTypeDto>
    {
        public IQuarterlyReturnNoTypeDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
