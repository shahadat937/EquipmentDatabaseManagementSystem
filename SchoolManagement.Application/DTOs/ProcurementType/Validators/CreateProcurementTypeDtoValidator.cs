using FluentValidation;

namespace SchoolManagement.Application.DTOs.ProcurementType.Validators
{
    public class CreateProcurementTypeDtoValidator : AbstractValidator<CreateProcurementTypeDto>
    {
        public CreateProcurementTypeDtoValidator()
        {
            Include(new IProcurementTypeDtoValidator());
        }
    }
}
 