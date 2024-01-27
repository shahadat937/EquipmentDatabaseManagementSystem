using FluentValidation;

namespace SchoolManagement.Application.DTOs.Sqns.Validators
{
    public class UpdateSqnDtoValidator : AbstractValidator<SqnDto>
    {
        public UpdateSqnDtoValidator() 
        {
            Include(new ISqnDtoValidator());

            RuleFor(b => b.SqnId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
