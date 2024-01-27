
using FluentValidation;

namespace SchoolManagement.Application.DTOs.OperationalStates.Validators
{
    public class IOperationalStateDtoValidator : AbstractValidator<IOperationalStateDto>
    {
        public IOperationalStateDtoValidator()
        {
            //RuleFor(b => b.Name)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
