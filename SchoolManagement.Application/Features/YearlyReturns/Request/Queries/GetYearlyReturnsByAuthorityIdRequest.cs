using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.YearlyReturns;
using SchoolManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.YearlyReturns.Request.Queries
{
    public class GetYearlyReturnsByAuthorityIdRequest : IRequest<PagedResult<YearlyReturnDto>>
    {
        public QueryParams  QueryParams { get; set; }
        public int AuthorityId { get; set; }
    }
}
