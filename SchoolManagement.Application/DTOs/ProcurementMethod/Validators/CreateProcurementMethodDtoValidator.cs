using FluentValidation;

namespace SchoolManagement.Application.DTOs.ProcurementMethod.Validators
{
    public class CreateProcurementMethodDtoValidator : AbstractValidator<CreateProcurementMethodDto>
    {
        public CreateProcurementMethodDtoValidator()
        {
            Include(new IProcurementMethodDtoValidator());
        }
    }
}
 