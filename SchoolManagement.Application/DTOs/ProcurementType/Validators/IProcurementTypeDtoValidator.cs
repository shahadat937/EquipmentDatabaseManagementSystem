
using FluentValidation;

namespace SchoolManagement.Application.DTOs.ProcurementType.Validators
{
    public class IProcurementTypeDtoValidator : AbstractValidator<IProcurementTypeDto>
    {
        public IProcurementTypeDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
