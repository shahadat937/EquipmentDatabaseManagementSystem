
using FluentValidation;

namespace SchoolManagement.Application.DTOs.Brands.Validators
{
    public class IBrandDtoValidator : AbstractValidator<IBrandDto>
    {
        public IBrandDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
