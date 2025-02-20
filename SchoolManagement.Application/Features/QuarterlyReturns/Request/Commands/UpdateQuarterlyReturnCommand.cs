using MediatR;
using SchoolManagement.Application.DTOs.QuarterlyReturns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.QuarterlyReturns.Request.Commands
{
    public class UpdateQuarterlyReturnCommand:IRequest<Unit>
    {
        public CreateQuarterlyReturnDto QuarterlyReturnDto { get; set; }
    }
}
