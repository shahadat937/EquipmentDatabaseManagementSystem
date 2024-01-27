using FluentValidation;

namespace SchoolManagement.Application.DTOs.EqupmentNames.Validators
{
    public class UpdateEqupmentNameDtoValidator : AbstractValidator<EqupmentNameDto>
    {
        public UpdateEqupmentNameDtoValidator() 
        {
            Include(new IEqupmentNameDtoValidator());

            RuleFor(b => b.EqupmentNameId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
