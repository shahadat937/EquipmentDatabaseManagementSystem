using FluentValidation;

namespace SchoolManagement.Application.DTOs.Brands.Validators
{
    public class CreateBrandDtoValidator : AbstractValidator<CreateBrandDto>
    {
        public CreateBrandDtoValidator()
        {
            Include(new IBrandDtoValidator());
        }
    }
}
 