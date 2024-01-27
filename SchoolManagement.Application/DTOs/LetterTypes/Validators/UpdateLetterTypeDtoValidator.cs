using FluentValidation;

namespace SchoolManagement.Application.DTOs.LetterTypes.Validators
{
    public class UpdateLetterTypeDtoValidator : AbstractValidator<LetterTypeDto>
    {
        public UpdateLetterTypeDtoValidator() 
        {
            Include(new ILetterTypeDtoValidator());

            RuleFor(b => b.LetterTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
