
using FluentValidation;

namespace SchoolManagement.Application.DTOs.AcquisitionMethod.Validators
{
    public class IAcquisitionMethodDtoValidator : AbstractValidator<IAcquisitionMethodDto>
    {
        public IAcquisitionMethodDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
