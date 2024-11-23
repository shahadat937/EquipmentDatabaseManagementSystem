using MediatR;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using SchoolManagement.Application.DTOs.YearlyReturns;
using SchoolManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.YearlyReturns.Request.Commands
{
    public class CreateYearlyReturnCommand : IRequest<BaseCommandResponse>
    {
        public CreateYearlyReturnDto YearlyReturnDto { get; set; }
    }
}
