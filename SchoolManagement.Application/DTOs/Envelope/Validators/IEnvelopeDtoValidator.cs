
using FluentValidation;

namespace SchoolManagement.Application.DTOs.Envelope.Validators
{
    public class IEnvelopeDtoValidator : AbstractValidator<IEnvelopeDto>
    {
        public IEnvelopeDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
