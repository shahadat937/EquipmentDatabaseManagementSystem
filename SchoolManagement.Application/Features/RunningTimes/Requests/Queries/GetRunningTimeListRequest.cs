using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.RunningTimes;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.RunningTimes.Requests.Queries
{
    public class GetRunningTimeListRequest : IRequest<PagedResult<RunningTimeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
