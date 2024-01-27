using FluentValidation;

namespace SchoolManagement.Application.DTOs.TenderOpeningDateTypes.Validators
{
    public class CreateTenderOpeningDateTypeDtoValidator : AbstractValidator<CreateTenderOpeningDateTypeDto>
    {
        public CreateTenderOpeningDateTypeDtoValidator()
        {
            Include(new ITenderOpeningDateTypeDtoValidator());
        }
    }
}
 