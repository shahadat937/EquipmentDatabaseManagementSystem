using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.User.Validators
{
    public class IUserDtoValidator : AbstractValidator<IUserDto>
    {
        public IUserDtoValidator()
        {
            //RuleFor(p => p.RoleId)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull();

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            //RuleFor(p => p.Password)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MinimumLength(6).WithMessage("{PropertyName} must not More Than {ComparisonValue} characters.");

            //RuleFor(p => p.BranchInfoId)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull();

            //RuleFor(p => p.UserFullName)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            
            //RuleFor(p => p.PhoneNumber)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MinimumLength(10).WithMessage("{PropertyName} must not More Than {ComparisonValue} characters.");
            
            //RuleFor(p => p.Email)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
                        
        }
    }
}
