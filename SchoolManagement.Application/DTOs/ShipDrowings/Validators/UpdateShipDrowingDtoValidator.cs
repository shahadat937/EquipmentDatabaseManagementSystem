using FluentValidation;
using SchoolManagement.Application.DTOs.ShipDrowinges;

namespace SchoolManagement.Application.DTOs.ShipDrowings.Validators
{
    public class UpdateShipDrowingDtoValidator : AbstractValidator<CreateShipDrowingDto>
    {
        public UpdateShipDrowingDtoValidator()  
        {
            Include(new IShipDrowingDtoValidator());

            RuleFor(b => b.ShipDrowingId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
