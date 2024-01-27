
using FluentValidation;

namespace SchoolManagement.Application.DTOs.ProjectStatus.Validators
{
    public class IProjectStatusDtoValidator : AbstractValidator<IProjectStatusDto>
    {
        public IProjectStatusDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
