using FluentValidation;
using SchoolManagement.Application.DTOs.BudgetCode;

namespace SchoolManagement.Application.DTOs.BudgetCode.Validators
{
    public class CreateBudgetCodeDtoValidator : AbstractValidator<CreateBudgetCodeDto>
    {
        public CreateBudgetCodeDtoValidator()
        {
            Include(new IBudgetCodeDtoValidator());
        }
    }
}
 