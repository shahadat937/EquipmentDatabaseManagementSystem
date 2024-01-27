using FluentValidation;

namespace SchoolManagement.Application.DTOs.OperationalStatuss.Validators
{
    public class CreateOperationalStatusDtoValidator : AbstractValidator<CreateOperationalStatusDto>
    {
        public CreateOperationalStatusDtoValidator()
        {
            Include(new IOperationalStatusDtoValidator());
        }
    }
}
 