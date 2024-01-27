using FluentValidation;

namespace SchoolManagement.Application.DTOs.HalfYearlyRunningTime.Validators
{
    public class CreateHalfYearlyRunningTimeDtoValidator : AbstractValidator<CreateHalfYearlyRunningTimeDto>
    {
        public CreateHalfYearlyRunningTimeDtoValidator()
        {
            Include(new IHalfYearlyRunningTimeDtoValidator());
        }
    }
}
 