using FluentValidation;

namespace SchoolManagement.Application.DTOs.ShipInformations.Validators
{
    public class CreateShipInformationDtoValidator : AbstractValidator<CreateShipInformationDto>
    {
        public CreateShipInformationDtoValidator()
        {
            Include(new IShipInformationDtoValidator());
        }
    }
}
 