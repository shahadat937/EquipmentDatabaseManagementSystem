
using FluentValidation;

namespace SchoolManagement.Application.DTOs.EquipmentType.Validators
{
    public class IEquipmentTypeDtoValidator : AbstractValidator<IEquipmentTypeDto>
    {
        public IEquipmentTypeDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
