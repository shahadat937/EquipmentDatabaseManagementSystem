using FluentValidation;

namespace SchoolManagement.Application.DTOs.Procurement.Validators
{
    public class UpdateProcurementDtoValidator : AbstractValidator<CreateProcurementDto>
    {
        public UpdateProcurementDtoValidator() 
        {
            Include(new IProcurementDtoValidator());

            RuleFor(b => b.ProcurementId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
