using FluentValidation;

namespace SchoolManagement.Application.DTOs.EqupmentNames.Validators
{
    public class CreateEqupmentNameDtoValidator : AbstractValidator<CreateEqupmentNameDto>
    {
        public CreateEqupmentNameDtoValidator()
        {
            Include(new IEqupmentNameDtoValidator());
        }
    }
}
 