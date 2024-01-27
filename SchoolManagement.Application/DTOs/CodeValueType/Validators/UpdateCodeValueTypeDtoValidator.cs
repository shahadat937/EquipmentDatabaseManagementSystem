using FluentValidation;


namespace SchoolManagement.Application.DTOs.CodeValueType.Validators
{
    public class UpdateCodeValueTypeDtoValidator : AbstractValidator<CodeValueTypeDto>
    {
        public UpdateCodeValueTypeDtoValidator()
        {
            Include(new ICodeValueTypeDtoValidator());

            RuleFor(p => p.CodeValueTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
