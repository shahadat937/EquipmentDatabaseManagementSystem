using FluentValidation;

namespace SchoolManagement.Application.DTOs.RunningTimes.Validators
{
    public class UpdateRunningTimeDtoValidator : AbstractValidator<RunningTimeDto>
    {
        public UpdateRunningTimeDtoValidator() 
        {
            Include(new IRunningTimeDtoValidator());

            RuleFor(b => b.RunningTimeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
