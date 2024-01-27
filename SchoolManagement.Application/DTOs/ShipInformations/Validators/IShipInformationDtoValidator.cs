
using FluentValidation;

namespace SchoolManagement.Application.DTOs.ShipInformations.Validators
{
    public class IShipInformationDtoValidator : AbstractValidator<IShipInformationDto>
    {
        public IShipInformationDtoValidator()
        {
            //RuleFor(b => b.Name)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
