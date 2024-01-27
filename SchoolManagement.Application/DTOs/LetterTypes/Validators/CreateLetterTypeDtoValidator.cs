using FluentValidation;

namespace SchoolManagement.Application.DTOs.LetterTypes.Validators
{
    public class CreateLetterTypeDtoValidator : AbstractValidator<CreateLetterTypeDto>
    {
        public CreateLetterTypeDtoValidator()
        {
            Include(new ILetterTypeDtoValidator());
        }
    }
}
 