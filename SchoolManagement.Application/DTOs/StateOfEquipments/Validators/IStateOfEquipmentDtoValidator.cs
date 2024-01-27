
using FluentValidation;

namespace SchoolManagement.Application.DTOs.StateOfEquipments.Validators
{
    public class IStateOfEquipmentDtoValidator : AbstractValidator<IStateOfEquipmentDto>
    {
        public IStateOfEquipmentDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
