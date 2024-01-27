
using FluentValidation;
using SchoolManagement.Application.DTOs.ShipDrowinges;

namespace SchoolManagement.Application.DTOs.ShipDrowings.Validators
{
    public class IShipDrowingDtoValidator : AbstractValidator<IShipDrowingDto>
    {
        //public IShipDrowingDtoValidator()
        //{
        //    RuleFor(b => b.Name)
        //        .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        //}
    }
}
