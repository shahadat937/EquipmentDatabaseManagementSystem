using FluentValidation;

namespace SchoolManagement.Application.DTOs.HalfYearlyRunningTime.Validators
{
    public class UpdateHalfYearlyRunningTimeDtoValidator : AbstractValidator<HalfYearlyRunningTimeDto>
    {
        public UpdateHalfYearlyRunningTimeDtoValidator() 
        {
            Include(new IHalfYearlyRunningTimeDtoValidator());

            RuleFor(b => b.HalfYearlyRunningTimeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
