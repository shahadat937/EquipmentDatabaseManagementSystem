using FluentValidation;

namespace SchoolManagement.Application.DTOs.OperationalStates.Validators
{
    public class CreateOperationalStateDtoValidator : AbstractValidator<CreateOperationalStateDto>
    {
        public CreateOperationalStateDtoValidator()
        {
            Include(new IOperationalStateDtoValidator());
        }
    }
}
 