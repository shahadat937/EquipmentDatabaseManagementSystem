using MediatR;
using SchoolManagement.Application.DTOs.RunningTimes;

namespace SchoolManagement.Application.Features.RunningTimes.Requests.Queries
{
    public class GetRunningTimeDetailRequest : IRequest<RunningTimeDto>
    {
        public int RunningTimeId { get; set; }
    }
}
