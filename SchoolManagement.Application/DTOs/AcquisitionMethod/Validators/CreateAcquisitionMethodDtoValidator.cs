using FluentValidation;

namespace SchoolManagement.Application.DTOs.AcquisitionMethod.Validators
{
    public class CreateAcquisitionMethodDtoValidator : AbstractValidator<CreateAcquisitionMethodDto>
    {
        public CreateAcquisitionMethodDtoValidator()
        {
            Include(new IAcquisitionMethodDtoValidator());
        }
    }
}
 