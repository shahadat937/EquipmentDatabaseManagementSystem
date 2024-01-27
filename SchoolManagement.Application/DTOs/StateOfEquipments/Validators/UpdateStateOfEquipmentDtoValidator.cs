using FluentValidation;

namespace SchoolManagement.Application.DTOs.StateOfEquipments.Validators
{
    public class UpdateStateOfEquipmentDtoValidator : AbstractValidator<StateOfEquipmentDto>
    {
        public UpdateStateOfEquipmentDtoValidator() 
        {
            Include(new IStateOfEquipmentDtoValidator());

            RuleFor(b => b.StateOfEquipmentId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
