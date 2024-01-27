using FluentValidation;

namespace SchoolManagement.Application.DTOs.MonthlyReturns.Validators
{
    public class UpdateMonthlyReturnDtoValidator : AbstractValidator<MonthlyReturnDto>
    {
        public UpdateMonthlyReturnDtoValidator() 
        {
            Include(new IMonthlyReturnDtoValidator());

            RuleFor(b => b.MonthlyReturnId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
