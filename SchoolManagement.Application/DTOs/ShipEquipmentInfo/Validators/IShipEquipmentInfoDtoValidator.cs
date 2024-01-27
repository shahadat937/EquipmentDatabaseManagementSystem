
using FluentValidation;

namespace SchoolManagement.Application.DTOs.ShipEquipmentInfo.Validators
{
    public class IShipEquipmentInfoDtoValidator : AbstractValidator<IShipEquipmentInfoDto>
    {
        public IShipEquipmentInfoDtoValidator()
        {
            //RuleFor(b => b.Model)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
