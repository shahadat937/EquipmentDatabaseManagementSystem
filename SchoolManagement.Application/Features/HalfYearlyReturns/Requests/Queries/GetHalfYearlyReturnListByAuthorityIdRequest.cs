using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.HalfYearlyReturn;
using SchoolManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Queries
{
    public class GetHalfYearlyReturnListByAuthorityIdRequest : IRequest<PagedResult<HalfYearlyReturnDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int AuthorityId { get; set; }
    }
}
