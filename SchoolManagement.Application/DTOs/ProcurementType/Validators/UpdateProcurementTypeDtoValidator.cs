using FluentValidation;

namespace SchoolManagement.Application.DTOs.ProcurementType.Validators
{
    public class UpdateProcurementTypeDtoValidator : AbstractValidator<ProcurementTypeDto>
    {
        public UpdateProcurementTypeDtoValidator() 
        {
            Include(new IProcurementTypeDtoValidator());

            RuleFor(b => b.ProcurementTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
