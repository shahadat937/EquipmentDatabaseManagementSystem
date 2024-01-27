
using FluentValidation;

namespace SchoolManagement.Application.DTOs.DealingOfficer.Validators
{
    public class IDealingOfficerDtoValidator : AbstractValidator<IDealingOfficerDto>
    {
        public IDealingOfficerDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
