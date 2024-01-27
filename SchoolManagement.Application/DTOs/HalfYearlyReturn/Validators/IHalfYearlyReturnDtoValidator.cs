
using FluentValidation;

namespace SchoolManagement.Application.DTOs.HalfYearlyReturn.Validators
{
    public class IHalfYearlyReturnDtoValidator : AbstractValidator<IHalfYearlyReturnDto>
    {
        public IHalfYearlyReturnDtoValidator()
        {
            //RuleFor(b => b.InputPowerSupply)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
