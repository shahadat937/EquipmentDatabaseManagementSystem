using FluentValidation;

namespace SchoolManagement.Application.DTOs.EquipmentType.Validators
{
    public class CreateEquipmentTypeDtoValidator : AbstractValidator<CreateEquipmentTypeDto>
    {
        public CreateEquipmentTypeDtoValidator()
        {
            Include(new IEquipmentTypeDtoValidator());
        }
    }
}
 