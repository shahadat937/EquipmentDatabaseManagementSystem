using MediatR;
using SchoolManagement.Application.DTOs.RunningTimes;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.RunningTimes.Requests.Commands
{
    public class CreateRunningTimeCommand : IRequest<BaseCommandResponse>
    {
        public CreateRunningTimeDto RunningTimeDto { get; set; }
    }
}
