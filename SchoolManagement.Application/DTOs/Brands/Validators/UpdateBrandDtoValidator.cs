using FluentValidation;

namespace SchoolManagement.Application.DTOs.Brands.Validators
{
    public class UpdateBrandDtoValidator : AbstractValidator<BrandDto>
    {
        public UpdateBrandDtoValidator() 
        {
            Include(new IBrandDtoValidator());

            RuleFor(b => b.BrandId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
