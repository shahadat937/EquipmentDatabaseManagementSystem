using FluentValidation;

namespace SchoolManagement.Application.DTOs.BookUserManualBRInfo.Validators
{
    public class UpdateBookUserManualBRInfoDtoValidator : AbstractValidator<BookUserManualBRInfoDto>
    {
        public UpdateBookUserManualBRInfoDtoValidator() 
        {
            Include(new IBookUserManualBRInfoDtoValidator());

            RuleFor(b => b.BookUserManualBRInfoId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
