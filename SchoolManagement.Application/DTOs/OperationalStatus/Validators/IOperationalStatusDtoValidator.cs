
using FluentValidation;

namespace SchoolManagement.Application.DTOs.OperationalStatuss.Validators
{
    public class IOperationalStatusDtoValidator : AbstractValidator<IOperationalStatusDto>
    {
        public IOperationalStatusDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
