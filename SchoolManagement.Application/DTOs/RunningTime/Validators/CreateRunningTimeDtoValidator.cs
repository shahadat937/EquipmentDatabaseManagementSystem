using FluentValidation;

namespace SchoolManagement.Application.DTOs.RunningTimes.Validators
{
    public class CreateRunningTimeDtoValidator : AbstractValidator<CreateRunningTimeDto>
    {
        public CreateRunningTimeDtoValidator()
        {
            Include(new IRunningTimeDtoValidator());
        }
    }
}
 