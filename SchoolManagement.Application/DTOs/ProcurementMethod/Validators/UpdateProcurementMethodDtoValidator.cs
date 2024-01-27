using FluentValidation;

namespace SchoolManagement.Application.DTOs.ProcurementMethod.Validators
{
    public class UpdateProcurementMethodDtoValidator : AbstractValidator<ProcurementMethodDto>
    {
        public UpdateProcurementMethodDtoValidator() 
        {
            Include(new IProcurementMethodDtoValidator());

            RuleFor(b => b.ProcurementMethodId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
