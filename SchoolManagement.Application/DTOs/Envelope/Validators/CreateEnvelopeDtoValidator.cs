using FluentValidation;

namespace SchoolManagement.Application.DTOs.Envelope.Validators
{
    public class CreateEnvelopeDtoValidator : AbstractValidator<CreateEnvelopeDto>
    {
        public CreateEnvelopeDtoValidator()
        {
            Include(new IEnvelopeDtoValidator());
        }
    }
}
 