using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.YearSetup.Validators
{
    public class CreateYearSetupDtoValidator : AbstractValidator<CreateYearSetupDto>
    {
        public CreateYearSetupDtoValidator() 
        {
            Include(new IYearSetupDtoValidator());
        }
    }
} 
