using FluentValidation;

namespace SchoolManagement.Application.DTOs.GroupNames.Validators
{
    public class CreateGroupNameDtoValidator : AbstractValidator<CreateGroupNameDto>
    {
        public CreateGroupNameDtoValidator()
        {
            Include(new IGroupNameDtoValidator());
        }
    }
}
 