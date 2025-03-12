
using FluentValidation;
using SchoolManagement.Application.DTOs.Budget;

namespace SchoolManagement.Application.DTOs.Budget.Validators
{
    public class IBudgetDtoValidator : AbstractValidator<IBudgetDto>
    {
        public IBudgetDtoValidator()
        {
            //RuleFor(b => b.Name)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
