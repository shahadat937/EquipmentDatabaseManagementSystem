using FluentValidation;

namespace SchoolManagement.Application.DTOs.DailyWorkState.Validators
{
    public class UpdateDailyWorkStateDtoValidator : AbstractValidator<CreateDailyWorkStateDto>
    {
        public UpdateDailyWorkStateDtoValidator() 
        {
            Include(new IDailyWorkStateDtoValidator());

            RuleFor(b => b.DailyWorkStateId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
