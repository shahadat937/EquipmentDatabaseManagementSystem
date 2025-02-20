using MediatR;
using SchoolManagement.Application.DTOs.QuarterlyReturns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.QuarterlyReturns.Request.Queries
{
    public class GetQuarterlyReturnDetailRequest : IRequest<QuarterlyReturnDto>
    {
        public int QuarterlyReturnId { get; set; }

        //public GetQuarterlyReturnDetailRequest(int QuarterlyReturnId)
        //{
        //    QuarterlyReturnId = QuarterlyReturnId;
        //}
    }
}
