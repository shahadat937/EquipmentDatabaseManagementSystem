using FluentValidation;

namespace SchoolManagement.Application.DTOs.AcquisitionMethod.Validators
{
    public class UpdateAcquisitionMethodDtoValidator : AbstractValidator<AcquisitionMethodDto>
    {
        public UpdateAcquisitionMethodDtoValidator() 
        {
            Include(new IAcquisitionMethodDtoValidator());

            RuleFor(b => b.AcquisitionMethodId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
