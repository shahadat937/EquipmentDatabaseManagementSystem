using FluentValidation;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.YearlyReturns.Validators
{
    public class IYearlyReturnDtoValidator : AbstractValidator<IYearlyReturnDto>
    {
        public IYearlyReturnDtoValidator()
        {
            
        }
    }
}
