using FluentValidation;

namespace SchoolManagement.Application.DTOs.BookType.Validators
{
    public class CreateBookTypeDtoValidator : AbstractValidator<CreateBookTypeDto>
    {
        public CreateBookTypeDtoValidator()
        {
            Include(new IBookTypeDtoValidator());
        }
    }
}
 