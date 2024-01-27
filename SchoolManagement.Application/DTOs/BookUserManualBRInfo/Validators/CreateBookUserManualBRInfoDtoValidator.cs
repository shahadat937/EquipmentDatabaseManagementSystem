using FluentValidation;

namespace SchoolManagement.Application.DTOs.BookUserManualBRInfo.Validators
{
    public class CreateBookUserManualBRInfoDtoValidator : AbstractValidator<CreateBookUserManualBRInfoDto>
    {
        public CreateBookUserManualBRInfoDtoValidator()
        {
            Include(new IBookUserManualBRInfoDtoValidator());
        }
    }
}
 