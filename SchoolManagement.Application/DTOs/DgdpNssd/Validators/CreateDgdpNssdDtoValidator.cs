using FluentValidation;

namespace SchoolManagement.Application.DTOs.DgdpNssd.Validators
{
    public class CreateDgdpNssdDtoValidator : AbstractValidator<CreateDgdpNssdDto>
    {
        public CreateDgdpNssdDtoValidator()
        {
            Include(new IDgdpNssdDtoValidator());
        }
    }
}
 