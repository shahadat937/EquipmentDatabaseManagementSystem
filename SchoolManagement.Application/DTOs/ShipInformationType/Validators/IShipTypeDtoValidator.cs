
using FluentValidation;
using SchoolManagement.Application.DTOs.ShipInformationTypes;

namespace SchoolManagement.Application.DTOs.ShipTypeDtos.Validators
{
    public class IShipTypeDtoValidator : AbstractValidator<IShipTypeDto>
    {
        public IShipTypeDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
