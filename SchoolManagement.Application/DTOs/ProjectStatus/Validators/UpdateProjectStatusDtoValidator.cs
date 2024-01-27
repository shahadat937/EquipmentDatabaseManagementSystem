using FluentValidation;

namespace SchoolManagement.Application.DTOs.ProjectStatus.Validators
{
    public class UpdateProjectStatusDtoValidator : AbstractValidator<ProjectStatusDto>
    {
        public UpdateProjectStatusDtoValidator() 
        {
            Include(new IProjectStatusDtoValidator());

            RuleFor(b => b.ProjectStatusId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
