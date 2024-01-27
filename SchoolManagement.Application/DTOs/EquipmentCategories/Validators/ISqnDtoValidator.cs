
using FluentValidation;

namespace SchoolManagement.Application.DTOs.EquipmentCategorys.Validators
{
    public class IEquipmentCategoryDtoValidator : AbstractValidator<IEquipmentCategoryDto>
    {
        public IEquipmentCategoryDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
