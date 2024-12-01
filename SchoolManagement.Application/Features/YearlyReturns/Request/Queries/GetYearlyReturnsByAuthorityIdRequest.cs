using MediatR;
using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.YearlyReturns.Request.Queries
{
    public class GetYearlyReturnsByAuthorityIdRequest : IRequest<object>
    {
        public QueryParams  QueryParams { get; set; }
        public int AuthorityId { get; set; }
    }
}
