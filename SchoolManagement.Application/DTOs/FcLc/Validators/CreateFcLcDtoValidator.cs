using FluentValidation;

namespace SchoolManagement.Application.DTOs.FcLc.Validators
{
    public class CreateFcLcDtoValidator : AbstractValidator<CreateFcLcDto>
    {
        public CreateFcLcDtoValidator()
        {
            Include(new IFcLcDtoValidator());
        }
    }
}
 