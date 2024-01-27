using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.HalfYearlyReturn;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Queries
{
    public class GetHalfYearlyReturnListRequest : IRequest<PagedResult<HalfYearlyReturnDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
