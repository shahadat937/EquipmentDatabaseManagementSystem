using MediatR;
using SchoolManagement.Application.DTOs.HalfYearlyRunningTime;

namespace SchoolManagement.Application.Features.HalfYearlyRunningTimes.Requests.Commands
{
    public class UpdateHalfYearlyRunningTimeCommand : IRequest<Unit>
    { 
        public HalfYearlyRunningTimeDto HalfYearlyRunningTimeDto { get; set; }
    }
}
 