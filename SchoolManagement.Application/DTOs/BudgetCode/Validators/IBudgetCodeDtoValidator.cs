
using FluentValidation;
using SchoolManagement.Application.DTOs.BudgetCode;

namespace SchoolManagement.Application.DTOs.BudgetCode.Validators
{
    public class IBudgetCodeDtoValidator : AbstractValidator<IBudgetCodeDto>
    {
        public IBudgetCodeDtoValidator()
        {

            RuleFor(b => b.Code)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
