using FluentValidation;

namespace SchoolManagement.Application.DTOs.ShipEquipmentInfo.Validators
{
    public class UpdateShipEquipmentInfoDtoValidator : AbstractValidator<ShipEquipmentInfoDto>
    {
        public UpdateShipEquipmentInfoDtoValidator() 
        {
            Include(new IShipEquipmentInfoDtoValidator());

            RuleFor(b => b.ShipEquipmentInfoId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
