using FluentValidation;

namespace SchoolManagement.Application.DTOs.QuarterlyReturnNoTypes.Validators
{
    public class CreateQuarterlyReturnNoTypeDtoValidator : AbstractValidator<CreateQuarterlyReturnNoTypeDto>
    {
        public CreateQuarterlyReturnNoTypeDtoValidator()
        {
            Include(new IQuarterlyReturnNoTypeDtoValidator());
        }
    }
}
 