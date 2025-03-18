using FluentValidation;

namespace SchoolManagement.Application.DTOs.Project.Validators
{
    public class UpdateProjectDtoValidator : AbstractValidator<CreateProjectDto>
    {
        public UpdateProjectDtoValidator() 
        {
            Include(new IProjectDtoValidator());

            RuleFor(b => b.ProjectId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
