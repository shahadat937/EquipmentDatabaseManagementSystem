using MediatR;

namespace SchoolManagement.Application.Features.DailyWorkStates.Requests.Commands
{
    public class DeleteDailyWorkStateCommand : IRequest
    {
        public int DailyWorkStateId { get; set; }
    }
} 
