using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.RoleFeature.Validators
{
    public class UpdateRoleFeatureDtoValidator : AbstractValidator<RoleFeatureDto>
    {
        public UpdateRoleFeatureDtoValidator()
        {
            Include(new IRoleFeatureDtoValidator());

            RuleFor(p => p.FeatureId).NotNull().WithMessage("{PropertyName} must be present");
            RuleFor(p => p.RoleId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
