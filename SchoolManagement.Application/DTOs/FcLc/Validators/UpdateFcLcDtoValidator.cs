using FluentValidation;

namespace SchoolManagement.Application.DTOs.FcLc.Validators
{
    public class UpdateFcLcDtoValidator : AbstractValidator<FcLcDto>
    {
        public UpdateFcLcDtoValidator() 
        {
            Include(new IFcLcDtoValidator());

            RuleFor(b => b.FcLcId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
