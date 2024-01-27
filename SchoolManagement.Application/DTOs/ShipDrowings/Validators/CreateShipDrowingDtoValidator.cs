using FluentValidation;
using SchoolManagement.Application.DTOs.ShipDrowinges;

namespace SchoolManagement.Application.DTOs.ShipDrowings.Validators
{
    public class CreateShipDrowingDtoValidator : AbstractValidator<CreateShipDrowingDto>
    {
        public CreateShipDrowingDtoValidator()
        {
            Include(new IShipDrowingDtoValidator());
        }
    }
} 
 