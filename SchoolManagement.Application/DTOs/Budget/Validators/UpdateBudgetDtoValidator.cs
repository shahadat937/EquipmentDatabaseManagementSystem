using FluentValidation;

namespace SchoolManagement.Application.DTOs.Budget.Validators
{
    public class UpdateBudgetDtoValidator : AbstractValidator<CreateBudgetDto>
    {
        public UpdateBudgetDtoValidator() 
        {
            Include(new IBudgetDtoValidator());

            RuleFor(b => b.BudgetId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
