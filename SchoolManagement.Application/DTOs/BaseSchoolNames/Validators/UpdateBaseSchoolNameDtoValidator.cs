using FluentValidation;

namespace SchoolManagement.Application.DTOs.BaseSchoolNames.Validators
{
    public class UpdateBaseSchoolNameDtoValidator : AbstractValidator<BaseSchoolNameDto>
    {
        public UpdateBaseSchoolNameDtoValidator()
        {
            Include(new IBaseSchoolNameDtoValidator()); 

            RuleFor(p => p.BaseSchoolNameId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
