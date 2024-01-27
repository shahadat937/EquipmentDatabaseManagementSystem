using FluentValidation;

namespace SchoolManagement.Application.DTOs.Tec.Validators
{
    public class UpdateTecDtoValidator : AbstractValidator<TecDto>
    {
        public UpdateTecDtoValidator() 
        {
            Include(new ITecDtoValidator());

            RuleFor(b => b.TecId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
