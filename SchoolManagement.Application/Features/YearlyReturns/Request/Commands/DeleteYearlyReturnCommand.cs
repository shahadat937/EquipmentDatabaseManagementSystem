using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.YearlyReturns.Request.Commands
{
    public class DeleteYearlyReturnCommand : IRequest
    {
        public int YearlyReturnId { get; set; }

        public DeleteYearlyReturnCommand(int yearlyReturnId)
        {
            YearlyReturnId = yearlyReturnId;
        }
    }

}
