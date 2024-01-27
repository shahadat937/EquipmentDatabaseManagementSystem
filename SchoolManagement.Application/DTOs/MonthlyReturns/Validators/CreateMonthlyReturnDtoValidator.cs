using FluentValidation;

namespace SchoolManagement.Application.DTOs.MonthlyReturns.Validators
{
    public class CreateMonthlyReturnDtoValidator : AbstractValidator<CreateMonthlyReturnDto>
    {
        public CreateMonthlyReturnDtoValidator()
        {
            Include(new IMonthlyReturnDtoValidator());
        }
    }
}
 