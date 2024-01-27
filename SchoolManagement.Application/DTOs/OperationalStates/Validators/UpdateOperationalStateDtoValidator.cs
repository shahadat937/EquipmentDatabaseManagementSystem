using FluentValidation;

namespace SchoolManagement.Application.DTOs.OperationalStates.Validators
{
    public class UpdateOperationalStateDtoValidator : AbstractValidator<OperationalStateDto>
    {
        public UpdateOperationalStateDtoValidator() 
        {
            Include(new IOperationalStateDtoValidator());

            RuleFor(b => b.OperationalStateId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
