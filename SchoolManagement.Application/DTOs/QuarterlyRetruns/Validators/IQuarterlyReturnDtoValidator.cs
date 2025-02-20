using FluentValidation;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.QuarterlyReturns.Validators
{
    public class IQuarterlyReturnDtoValidator : AbstractValidator<IQuarterlyReturnDto>
    {
        public IQuarterlyReturnDtoValidator()
        {
            
        }
    }
}
