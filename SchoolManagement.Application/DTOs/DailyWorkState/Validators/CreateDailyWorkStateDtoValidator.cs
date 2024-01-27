using FluentValidation;

namespace SchoolManagement.Application.DTOs.DailyWorkState.Validators
{
    public class CreateDailyWorkStateDtoValidator : AbstractValidator<CreateDailyWorkStateDto>
    {
        public CreateDailyWorkStateDtoValidator()
        {
            Include(new IDailyWorkStateDtoValidator());
        }
    }
}
 