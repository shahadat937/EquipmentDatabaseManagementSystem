using FluentValidation;

namespace SchoolManagement.Application.DTOs.BookType.Validators
{
    public class UpdateBookTypeDtoValidator : AbstractValidator<BookTypeDto>
    {
        public UpdateBookTypeDtoValidator() 
        {
            Include(new IBookTypeDtoValidator());

            RuleFor(b => b.BookTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
