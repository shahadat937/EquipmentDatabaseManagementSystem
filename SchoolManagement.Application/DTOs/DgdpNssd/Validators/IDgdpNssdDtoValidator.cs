
using FluentValidation;

namespace SchoolManagement.Application.DTOs.DgdpNssd.Validators
{
    public class IDgdpNssdDtoValidator : AbstractValidator<IDgdpNssdDto>
    {
        public IDgdpNssdDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
