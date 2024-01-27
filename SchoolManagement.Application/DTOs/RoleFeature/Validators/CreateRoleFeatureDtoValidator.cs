using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.RoleFeature.Validators
{
    public class CreateRoleFeatureDtoValidator:AbstractValidator<CreateRoleFeatureDto>
    {
        public CreateRoleFeatureDtoValidator()
        {
            Include(new IRoleFeatureDtoValidator());
        }
    }
}
