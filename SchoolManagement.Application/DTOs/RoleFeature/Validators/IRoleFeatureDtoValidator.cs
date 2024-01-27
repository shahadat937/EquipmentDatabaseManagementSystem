using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.RoleFeature.Validators
{
    public class IRoleFeatureDtoValidator : AbstractValidator<IRoleFeatureDto>
    {
        public IRoleFeatureDtoValidator()
        {
            //RuleFor(p => p.RoleId)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull();

            //RuleFor(p => p.FeatureId)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull();
        }
    }
}
