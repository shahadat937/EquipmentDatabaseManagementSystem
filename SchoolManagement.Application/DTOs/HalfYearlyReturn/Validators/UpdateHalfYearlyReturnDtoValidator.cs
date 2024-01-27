using FluentValidation;

namespace SchoolManagement.Application.DTOs.HalfYearlyReturn.Validators
{
    public class UpdateHalfYearlyReturnDtoValidator : AbstractValidator<HalfYearlyReturnDto>
    {
        public UpdateHalfYearlyReturnDtoValidator() 
        {
            Include(new IHalfYearlyReturnDtoValidator());

            RuleFor(b => b.HalfYearlyReturnId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
