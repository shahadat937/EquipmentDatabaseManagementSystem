
using FluentValidation;

namespace SchoolManagement.Application.DTOs.ProcurementMethod.Validators
{
    public class IProcurementMethodDtoValidator : AbstractValidator<IProcurementMethodDto>
    {
        public IProcurementMethodDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
