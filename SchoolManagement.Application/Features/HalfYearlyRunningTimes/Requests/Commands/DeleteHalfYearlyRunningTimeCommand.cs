using MediatR;

namespace SchoolManagement.Application.Features.HalfYearlyRunningTimes.Requests.Commands
{
    public class DeleteHalfYearlyRunningTimeCommand : IRequest
    {
        public int HalfYearlyRunningTimeId { get; set; }
    }
} 
