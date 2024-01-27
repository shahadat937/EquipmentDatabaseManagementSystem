using FluentValidation;
using SchoolManagement.Application.DTOs.ShipInformationTypes;

namespace SchoolManagement.Application.DTOs.ShipTypeDtos.Validators
{
    public class UpdateShipTypeDtoValidator : AbstractValidator<ShipTypeDto>
    {
        public UpdateShipTypeDtoValidator() 
        {
            Include(new IShipTypeDtoValidator());

            RuleFor(b => b.ShipTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
