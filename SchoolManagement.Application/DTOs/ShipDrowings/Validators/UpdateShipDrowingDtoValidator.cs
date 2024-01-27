using FluentValidation;

namespace SchoolManagement.Application.DTOs.ShipDrowings.Validators
{
    public class UpdateShipDrowingDtoValidator : AbstractValidator<ShipDrowingDto>
    {
        public UpdateShipDrowingDtoValidator()  
        {
            Include(new IShipDrowingDtoValidator());

            RuleFor(b => b.ShipDrowingId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
