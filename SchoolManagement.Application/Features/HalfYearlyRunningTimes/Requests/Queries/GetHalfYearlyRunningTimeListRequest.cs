using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.HalfYearlyRunningTime;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.HalfYearlyRunningTimes.Requests.Queries
{
    public class GetHalfYearlyRunningTimeListRequest : IRequest<PagedResult<HalfYearlyRunningTimeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
