using FluentValidation;

namespace SchoolManagement.Application.DTOs.Sqns.Validators
{
    public class CreateSqnDtoValidator : AbstractValidator<CreateSqnDto>
    {
        public CreateSqnDtoValidator()
        {
            Include(new ISqnDtoValidator());
        }
    }
}
 