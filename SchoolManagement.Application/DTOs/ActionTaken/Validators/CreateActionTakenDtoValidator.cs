using FluentValidation;

namespace SchoolManagement.Application.DTOs.ActionTaken.Validators
{
    public class CreateActionTakenDtoValidator : AbstractValidator<CreateActionTakenDto>
    {
        public CreateActionTakenDtoValidator()
        {
            Include(new IActionTakenDtoValidator());
        }
    }
}
 