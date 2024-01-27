
using FluentValidation;

namespace SchoolManagement.Application.DTOs.PaymentStatus.Validators
{
    public class IPaymentStatusDtoValidator : AbstractValidator<IPaymentStatusDto>
    {
        public IPaymentStatusDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
