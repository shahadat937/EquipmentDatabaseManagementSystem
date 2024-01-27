using FluentValidation;

namespace SchoolManagement.Application.DTOs.DealingOfficer.Validators
{
    public class UpdateDealingOfficerDtoValidator : AbstractValidator<DealingOfficerDto>
    {
        public UpdateDealingOfficerDtoValidator() 
        {
            Include(new IDealingOfficerDtoValidator());

            RuleFor(b => b.DealingOfficerId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
