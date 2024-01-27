using FluentValidation;

namespace SchoolManagement.Application.DTOs.QuarterlyReturnNoTypes.Validators
{
    public class UpdateQuarterlyReturnNoTypeDtoValidator : AbstractValidator<QuarterlyReturnNoTypeDto>
    {
        public UpdateQuarterlyReturnNoTypeDtoValidator() 
        {
            Include(new IQuarterlyReturnNoTypeDtoValidator());

            RuleFor(b => b.QuarterlyReturnNoTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
