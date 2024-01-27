using FluentValidation;

namespace SchoolManagement.Application.DTOs.DgdpNssd.Validators
{
    public class UpdateDgdpNssdDtoValidator : AbstractValidator<DgdpNssdDto>
    {
        public UpdateDgdpNssdDtoValidator() 
        {
            Include(new IDgdpNssdDtoValidator());

            RuleFor(b => b.DgdpNssdId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
