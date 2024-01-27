using FluentValidation;

namespace SchoolManagement.Application.DTOs.ShipInformations.Validators
{
    public class UpdateShipInformationDtoValidator : AbstractValidator<ShipInformationDto>
    {
        public UpdateShipInformationDtoValidator() 
        {
            Include(new IShipInformationDtoValidator());

            RuleFor(b => b.ShipInformationId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
