using MediatR;
using SchoolManagement.Application.DTOs.RunningTimes;

namespace SchoolManagement.Application.Features.RunningTimes.Requests.Commands
{
    public class UpdateRunningTimeCommand : IRequest<Unit>
    { 
        public RunningTimeDto RunningTimeDto { get; set; }
    }
}
 