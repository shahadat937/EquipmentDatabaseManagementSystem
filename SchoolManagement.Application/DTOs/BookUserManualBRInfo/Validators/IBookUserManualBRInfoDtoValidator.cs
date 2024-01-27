
using FluentValidation;

namespace SchoolManagement.Application.DTOs.BookUserManualBRInfo.Validators
{
    public class IBookUserManualBRInfoDtoValidator : AbstractValidator<IBookUserManualBRInfoDto>
    {
        public IBookUserManualBRInfoDtoValidator()
        {
            RuleFor(b => b.BookName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
