
using FluentValidation;

namespace SchoolManagement.Application.DTOs.TenderOpeningDateTypes.Validators
{
    public class ITenderOpeningDateTypeDtoValidator : AbstractValidator<ITenderOpeningDateTypeDto>
    {
        public ITenderOpeningDateTypeDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
