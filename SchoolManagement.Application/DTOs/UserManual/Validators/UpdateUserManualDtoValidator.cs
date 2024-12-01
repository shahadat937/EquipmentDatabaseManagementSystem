using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.UserManual.Validators
{
    public class UpdateUserManualDtoValidator:AbstractValidator<UserManualDto>
    {
        public UpdateUserManualDtoValidator()
        {
            Include(new IUserManualDtoValidator());

            RuleFor(c => c.RoleName).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
