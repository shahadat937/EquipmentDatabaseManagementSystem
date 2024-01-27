using FluentValidation;

namespace SchoolManagement.Application.DTOs.PaymentStatus.Validators
{
    public class UpdatePaymentStatusDtoValidator : AbstractValidator<PaymentStatusDto>
    {
        public UpdatePaymentStatusDtoValidator() 
        {
            Include(new IPaymentStatusDtoValidator());

            RuleFor(b => b.PaymentStatusId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
