using FluentValidation;

namespace SchoolManagement.Application.DTOs.Priority.Validators
{
    public class UpdatePriorityDtoValidator : AbstractValidator<PriorityDto>
    {
        public UpdatePriorityDtoValidator() 
        {
            Include(new IPriorityDtoValidator());

            RuleFor(b => b.PriorityId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
