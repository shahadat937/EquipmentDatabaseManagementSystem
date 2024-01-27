using FluentValidation;

namespace SchoolManagement.Application.DTOs.Controlled.Validators
{
    public class CreateControlledDtoValidator : AbstractValidator<CreateControlledDto>
    {
        public CreateControlledDtoValidator()
        {
            Include(new IControlledDtoValidator());
        }
    }
}
 