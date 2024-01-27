using FluentValidation;

namespace SchoolManagement.Application.DTOs.ActionTaken.Validators
{
    public class UpdateActionTakenDtoValidator : AbstractValidator<ActionTakenDto>
    {
        public UpdateActionTakenDtoValidator() 
        {
            Include(new IActionTakenDtoValidator());

            RuleFor(b => b.ActionTakenId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
