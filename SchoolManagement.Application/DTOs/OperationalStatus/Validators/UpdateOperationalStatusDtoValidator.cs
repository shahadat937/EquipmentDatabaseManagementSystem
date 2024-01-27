using FluentValidation;

namespace SchoolManagement.Application.DTOs.OperationalStatuss.Validators
{
    public class UpdateOperationalStatusDtoValidator : AbstractValidator<OperationalStatusDto>
    {
        public UpdateOperationalStatusDtoValidator() 
        {
            Include(new IOperationalStatusDtoValidator());

            RuleFor(b => b.OperationalStatusId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
