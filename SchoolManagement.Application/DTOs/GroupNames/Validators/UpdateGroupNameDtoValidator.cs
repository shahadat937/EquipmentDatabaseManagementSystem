using FluentValidation;

namespace SchoolManagement.Application.DTOs.GroupNames.Validators
{
    public class UpdateGroupNameDtoValidator : AbstractValidator<GroupNameDto>
    {
        public UpdateGroupNameDtoValidator() 
        {
            Include(new IGroupNameDtoValidator());

            RuleFor(b => b.GroupNameId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
