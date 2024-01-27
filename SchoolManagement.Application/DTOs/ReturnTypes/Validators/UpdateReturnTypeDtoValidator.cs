using FluentValidation;

namespace SchoolManagement.Application.DTOs.ReturnTypes.Validators
{
    public class UpdateReturnTypeDtoValidator : AbstractValidator<ReturnTypeDto>
    {
        public UpdateReturnTypeDtoValidator() 
        {
            Include(new IReturnTypeDtoValidator());

            RuleFor(b => b.ReturnTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
