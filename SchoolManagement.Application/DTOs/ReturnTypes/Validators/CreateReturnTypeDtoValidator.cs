using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReturnTypes.Validators
{
    public class CreateReturnTypeDtoValidator : AbstractValidator<CreateReturnTypeDto>
    {
        public CreateReturnTypeDtoValidator()
        {
            Include(new IReturnTypeDtoValidator());
        }
    }
}
 