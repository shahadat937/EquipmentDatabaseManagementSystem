using FluentValidation;

namespace SchoolManagement.Application.DTOs.PaymentStatus.Validators
{
    public class CreatePaymentStatusDtoValidator : AbstractValidator<CreatePaymentStatusDto>
    {
        public CreatePaymentStatusDtoValidator()
        {
            Include(new IPaymentStatusDtoValidator());
        }
    }
}
 