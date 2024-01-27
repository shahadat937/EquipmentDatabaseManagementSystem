
using FluentValidation;

namespace SchoolManagement.Application.DTOs.MonthlyReturns.Validators
{
    public class IMonthlyReturnDtoValidator : AbstractValidator<IMonthlyReturnDto>
    {
        public IMonthlyReturnDtoValidator()
        {
            //RuleFor(b => b.Name)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
