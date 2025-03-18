using FluentValidation;

namespace SchoolManagement.Application.DTOs.Project.Validators
{
    public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectDtoValidator()
        {
            Include(new IProjectDtoValidator());
        }
    }
}
 