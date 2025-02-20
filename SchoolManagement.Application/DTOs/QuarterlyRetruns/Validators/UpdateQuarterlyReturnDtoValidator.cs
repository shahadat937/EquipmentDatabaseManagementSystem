using FluentValidation;
using SchoolManagement.Application.DTOs.MonthlyReturns.Validators;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.QuarterlyReturns.Validators
{
    public class UpdateQuarterlyReturnValidator : AbstractValidator<CreateQuarterlyReturnDto>
    {
        public UpdateQuarterlyReturnValidator()
        {
            //Include(new IQuarterlyReturnDtoValidator());

            RuleFor(b => b.QuarterlyReturnId).NotNull().WithMessage("{PropertyName} must be present");
        }

        private void Include(IQuarterlyReturnDtoValidator QuarterlyReturnDtoValidator)
        {
            throw new NotImplementedException();
        }
    }
}
