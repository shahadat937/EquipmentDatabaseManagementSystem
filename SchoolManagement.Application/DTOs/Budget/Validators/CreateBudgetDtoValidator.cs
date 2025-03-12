using FluentValidation;
using SchoolManagement.Application.DTOs.BudgetCode.Validators;

namespace SchoolManagement.Application.DTOs.Budget.Validators
{
    public class CreateBudgetDtoValidator : AbstractValidator<CreateBudgetDto>
    {
        public CreateBudgetDtoValidator()
        {
            Include(new IBudgetDtoValidator());
        }
    }
}
 