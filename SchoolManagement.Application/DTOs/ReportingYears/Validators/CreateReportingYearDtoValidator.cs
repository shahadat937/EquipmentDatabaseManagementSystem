using FluentValidation;
using SchoolManagement.Application.DTOs.ReportingYear;
using SchoolManagement.Application.DTOs.ReportingYear.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ReportingYear.Validators
{
    public class CreateReportingYearDtoValidator : AbstractValidator<CreateReportingYearDto>
    {
        public CreateReportingYearDtoValidator() 
        {
            Include(new IReportingYearDtoValidator());
        }
    }
} 
