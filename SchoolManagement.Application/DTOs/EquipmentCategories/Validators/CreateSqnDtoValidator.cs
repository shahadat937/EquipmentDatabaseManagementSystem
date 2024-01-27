using FluentValidation;

namespace SchoolManagement.Application.DTOs.EquipmentCategorys.Validators
{
    public class CreateEquipmentCategoryDtoValidator : AbstractValidator<CreateEquipmentCategoryDto>
    {
        public CreateEquipmentCategoryDtoValidator()
        {
            Include(new IEquipmentCategoryDtoValidator());
        }
    }
}
 