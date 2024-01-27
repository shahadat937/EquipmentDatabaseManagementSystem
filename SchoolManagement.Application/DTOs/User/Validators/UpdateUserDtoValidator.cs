using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.User.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UserDto>
    {
        public UpdateUserDtoValidator()
        {
            Include(new IUserDtoValidator());

            RuleFor(p => p.UserId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
