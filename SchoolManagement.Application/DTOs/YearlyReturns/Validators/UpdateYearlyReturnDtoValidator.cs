using FluentValidation;
using SchoolManagement.Application.DTOs.MonthlyReturns.Validators;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.YearlyReturns.Validators
{
    public class UpdateYearlyReturnValidator : AbstractValidator<YearlyReturnDto>
    {
        public UpdateYearlyReturnValidator()
        {
            Include(new IYearlyReturnDtoValidator());

            RuleFor(b => b.YearlyReturnId).NotNull().WithMessage("{PropertyName} must be present");
        }

        private void Include(IYearlyReturnDtoValidator yearlyReturnDtoValidator)
        {
            throw new NotImplementedException();
        }
    }
}
