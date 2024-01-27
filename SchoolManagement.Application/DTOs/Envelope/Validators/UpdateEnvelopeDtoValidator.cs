using FluentValidation;

namespace SchoolManagement.Application.DTOs.Envelope.Validators
{
    public class UpdateEnvelopeDtoValidator : AbstractValidator<EnvelopeDto>
    {
        public UpdateEnvelopeDtoValidator() 
        {
            Include(new IEnvelopeDtoValidator());

            RuleFor(b => b.EnvelopeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
