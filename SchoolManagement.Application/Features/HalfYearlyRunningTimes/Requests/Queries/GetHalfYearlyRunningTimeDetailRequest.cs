using MediatR;
using SchoolManagement.Application.DTOs.HalfYearlyRunningTime;

namespace SchoolManagement.Application.Features.HalfYearlyRunningTimes.Requests.Queries
{
    public class GetHalfYearlyRunningTimeDetailRequest : IRequest<HalfYearlyRunningTimeDto>
    {
        public int HalfYearlyRunningTimeId { get; set; }
    }
}
