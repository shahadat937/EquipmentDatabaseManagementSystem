using FluentValidation;

namespace SchoolManagement.Application.DTOs.TenderOpeningDateTypes.Validators
{
    public class UpdateTenderOpeningDateTypeDtoValidator : AbstractValidator<TenderOpeningDateTypeDto>
    {
        public UpdateTenderOpeningDateTypeDtoValidator() 
        {
            Include(new ITenderOpeningDateTypeDtoValidator());

            RuleFor(b => b.TenderOpeningDateTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
