using FluentValidation;

namespace SchoolManagement.Application.DTOs.HalfYearlyReturn.Validators
{
    public class CreateHalfYearlyReturnDtoValidator : AbstractValidator<CreateHalfYearlyReturnDto>
    {
        public CreateHalfYearlyReturnDtoValidator()
        {
            Include(new IHalfYearlyReturnDtoValidator());
        }
    }
}
 