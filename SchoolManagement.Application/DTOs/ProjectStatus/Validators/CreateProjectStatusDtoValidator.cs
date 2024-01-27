using FluentValidation;

namespace SchoolManagement.Application.DTOs.ProjectStatus.Validators
{
    public class CreateProjectStatusDtoValidator : AbstractValidator<CreateProjectStatusDto>
    {
        public CreateProjectStatusDtoValidator()
        {
            Include(new IProjectStatusDtoValidator());
        }
    }
}
 