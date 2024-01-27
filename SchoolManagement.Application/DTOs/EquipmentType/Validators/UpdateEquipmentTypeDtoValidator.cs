using FluentValidation;

namespace SchoolManagement.Application.DTOs.EquipmentType.Validators
{
    public class UpdateEquipmentTypeDtoValidator : AbstractValidator<EquipmentTypeDto>
    {
        public UpdateEquipmentTypeDtoValidator() 
        {
            Include(new IEquipmentTypeDtoValidator());

            RuleFor(b => b.EquipmentTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
