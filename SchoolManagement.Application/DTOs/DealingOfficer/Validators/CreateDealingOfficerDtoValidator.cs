using FluentValidation;

namespace SchoolManagement.Application.DTOs.DealingOfficer.Validators
{
    public class CreateDealingOfficerDtoValidator : AbstractValidator<CreateDealingOfficerDto>
    {
        public CreateDealingOfficerDtoValidator()
        {
            Include(new IDealingOfficerDtoValidator());
        }
    }
}
 