using MediatR;

namespace SchoolManagement.Application.Features.RunningTimes.Requests.Commands
{
    public class DeleteRunningTimeCommand : IRequest
    {
        public int RunningTimeId { get; set; }
    }
} 
