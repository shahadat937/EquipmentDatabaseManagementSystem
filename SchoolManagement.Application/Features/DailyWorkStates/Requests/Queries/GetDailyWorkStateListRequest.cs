using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.DailyWorkState;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.DailyWorkStates.Requests.Queries
{
    public class GetDailyWorkStateListRequest : IRequest<PagedResult<DailyWorkStateDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
