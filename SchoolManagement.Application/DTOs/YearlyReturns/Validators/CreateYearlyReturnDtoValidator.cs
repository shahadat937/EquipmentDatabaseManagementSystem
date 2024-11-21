using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.YearlyReturns.Validators
{
    public class CreateYearlyReturnDtoValidator:AbstractValidator<CreateYearlyReturnDto>
    {
        public CreateYearlyReturnDtoValidator()
        {
            Include(new IYearlyReturnDtoValidator());
        }
    }
}
