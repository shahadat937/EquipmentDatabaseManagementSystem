using MediatR;
using SchoolManagement.Application.DTOs.YearlyReturns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.YearlyReturns.Request.Queries
{
    public class GetYearlyReturnDetailRequest : IRequest<YearlyReturnDto>
    {
        public int YearlyReturnId { get; set; }

        //public GetYearlyReturnDetailRequest(int yearlyReturnId)
        //{
        //    YearlyReturnId = yearlyReturnId;
        //}
    }
}
