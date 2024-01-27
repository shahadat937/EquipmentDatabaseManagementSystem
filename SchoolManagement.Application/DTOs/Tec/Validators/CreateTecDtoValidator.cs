using FluentValidation;

namespace SchoolManagement.Application.DTOs.Tec.Validators
{
    public class CreateTecDtoValidator : AbstractValidator<CreateTecDto>
    {
        public CreateTecDtoValidator()
        {
            Include(new ITecDtoValidator());
        }
    }
}
 