
using FluentValidation;

namespace SchoolManagement.Application.DTOs.GroupNames.Validators
{
    public class IGroupNameDtoValidator : AbstractValidator<IGroupNameDto>
    {
        public IGroupNameDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
