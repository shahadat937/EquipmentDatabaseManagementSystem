
using FluentValidation;

namespace SchoolManagement.Application.DTOs.Procurement.Validators
{
    public class IProcurementDtoValidator : AbstractValidator<IProcurementDto>
    {
        public IProcurementDtoValidator()
        {
            //RuleFor(b => b.Name)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
