using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FinancialYears.Validators
{
    public class CreateFinancialYearsDtoValidator : AbstractValidator<CreateFinancialYearsDto>
    {
        public CreateFinancialYearsDtoValidator() 
        {
            Include(new IFinancialYearsValidator());
        }
    }
} 
