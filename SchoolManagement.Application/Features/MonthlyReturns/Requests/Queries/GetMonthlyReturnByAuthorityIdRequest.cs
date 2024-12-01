using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using SchoolManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.MonthlyReturns.Requests.Queries
{
    public class GetMonthlyReturnByAuthorityIdRequest : IRequest<PagedResult<MonthlyReturnDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int AuthorityId { get; set; }
    }
}
