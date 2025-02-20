using MediatR;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using SchoolManagement.Application.DTOs.QuarterlyReturns;
using SchoolManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.QuarterlyReturns.Request.Commands
{
    public class CreateQuarterlyReturnCommand : IRequest<BaseCommandResponse>
    {
        public CreateQuarterlyReturnDto QuarterlyReturnDto { get; set; }
    }
}
