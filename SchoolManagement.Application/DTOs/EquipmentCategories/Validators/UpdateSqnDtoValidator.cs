using FluentValidation;

namespace SchoolManagement.Application.DTOs.EquipmentCategorys.Validators
{
    public class UpdateEquipmentCategoryDtoValidator : AbstractValidator<EquipmentCategoryDto>
    {
        public UpdateEquipmentCategoryDtoValidator() 
        {
            Include(new IEquipmentCategoryDtoValidator());

            RuleFor(b => b.EquipmentCategoryId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
