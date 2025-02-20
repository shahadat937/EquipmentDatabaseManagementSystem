using MediatR;
using SchoolManagement.Application.DTOs.YearlyReturns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.YearlyReturns.Request.Commands
{
    public class UpdateYearlyReturnCommand:IRequest<Unit>
    {
        public CreateYearlyReturnDto YearlyReturnDto { get; set; }
    }
}
