
using FluentValidation;

namespace SchoolManagement.Application.DTOs.BookType.Validators
{
    public class IBookTypeDtoValidator : AbstractValidator<IBookTypeDto>
    {
        public IBookTypeDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
