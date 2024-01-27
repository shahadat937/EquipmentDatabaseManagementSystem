using FluentValidation;
using SchoolManagement.Application.DTOs.ShipInformationTypes;

namespace SchoolManagement.Application.DTOs.ShipTypeDtos.Validators
{
    public class CreateShipTypeDtoValidator : AbstractValidator<CreateShipTypeDto>
    {
        public CreateShipTypeDtoValidator()
        {
            Include(new IShipTypeDtoValidator());
        }
    }
}
 