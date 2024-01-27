using FluentValidation;

namespace SchoolManagement.Application.DTOs.Priority.Validators
{
    public class CreatePriorityDtoValidator : AbstractValidator<CreatePriorityDto>
    {
        public CreatePriorityDtoValidator()
        {
            Include(new IPriorityDtoValidator());
        }
    }
}
 