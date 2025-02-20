using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.QuarterlyReturns.Validators
{
    public class CreateQuarterlyReturnDtoValidator:AbstractValidator<CreateQuarterlyReturnDto>
    {
        public CreateQuarterlyReturnDtoValidator()
        {
            Include(new IQuarterlyReturnDtoValidator());
        }
    }
}
