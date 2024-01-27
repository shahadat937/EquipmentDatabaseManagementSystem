using FluentValidation;

namespace SchoolManagement.Application.DTOs.BaseSchoolNames.Validators
{
    public class IBaseSchoolNameDtoValidator : AbstractValidator<IBaseSchoolNameDto>
    {
        public IBaseSchoolNameDtoValidator()
        {
           // RuleFor(p => p.BaseNameId)
           //    .NotEmpty().WithMessage("{PropertyName} is required.");

           // RuleFor(p => p.ForceTypeId)
           //.NotEmpty().WithMessage("{PropertyName} is required.");

           // RuleFor(p => p.AdminAuthorityId)
           //.NotEmpty().WithMessage("{PropertyName} is required.");

           // RuleFor(p => p.SchoolName)
           //     .NotEmpty().WithMessage("{PropertyName} is required.")
           //     .NotNull()
           //     .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
