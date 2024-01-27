using FluentValidation;

namespace SchoolManagement.Application.DTOs.ShipEquipmentInfo.Validators
{
    public class CreateShipEquipmentInfoDtoValidator : AbstractValidator<CreateShipEquipmentInfoDto>
    {
        public CreateShipEquipmentInfoDtoValidator()
        {
            Include(new IShipEquipmentInfoDtoValidator());
        }
    }
}
 