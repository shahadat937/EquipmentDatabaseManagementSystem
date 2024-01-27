using FluentValidation;

namespace SchoolManagement.Application.DTOs.Controlled.Validators
{
    public class UpdateControlledDtoValidator : AbstractValidator<ControlledDto>
    {
        public UpdateControlledDtoValidator() 
        {
            Include(new IControlledDtoValidator());

            RuleFor(b => b.ControlledId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
