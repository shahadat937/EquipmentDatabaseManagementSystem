using FluentValidation;

namespace SchoolManagement.Application.DTOs.Procurement.Validators
{
    public class CreateProcurementDtoValidator : AbstractValidator<CreateProcurementDto>
    {
        public CreateProcurementDtoValidator()
        {
            Include(new IProcurementDtoValidator());
        }
    }
}
 