using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.QuarterlyReturns.Request.Commands
{
    public class DeleteQuarterlyReturnCommand : IRequest
    {
        public int QuarterlyReturnId { get; set; }

    }

}
