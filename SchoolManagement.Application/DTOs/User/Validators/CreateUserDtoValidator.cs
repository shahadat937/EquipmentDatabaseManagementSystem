using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.User.Validators
{
    public class CreateUserDtoValidator:AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            Include(new IUserDtoValidator());
        }
    }
}
