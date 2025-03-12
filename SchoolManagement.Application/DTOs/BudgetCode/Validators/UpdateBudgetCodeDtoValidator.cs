using FluentValidation;
using SchoolManagement.Application.DTOs.BudgetCode;
using SchoolManagement.Application.DTOs.BudgetCode.Validators;

namespace SchoolManagement.Application.DTOs.BudgetCodes.Validators
{
    public class UpdateBudgetCodeDtoValidator : AbstractValidator<BudgetCodeDto>
    {
        public UpdateBudgetCodeDtoValidator() 
        {
            Include(new IBudgetCodeDtoValidator());

            RuleFor(b => b.BudgetCodeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
