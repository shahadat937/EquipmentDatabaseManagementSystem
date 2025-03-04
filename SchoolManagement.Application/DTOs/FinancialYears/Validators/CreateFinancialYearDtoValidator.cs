using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FinancialYears.Validators
{
    public class CreateFinancialYearDtoValidator : AbstractValidator<CreateFinancialYearDto>
    {
        public CreateFinancialYearDtoValidator() 
        {
            Include(new IFinancialYearValidator());
        }
    }
} 
