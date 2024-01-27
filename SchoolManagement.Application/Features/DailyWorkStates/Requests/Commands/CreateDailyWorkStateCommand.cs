using MediatR;
using SchoolManagement.Application.DTOs.DailyWorkState;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.DailyWorkStates.Requests.Commands
{
    public class CreateDailyWorkStateCommand : IRequest<BaseCommandResponse>
    {
        public CreateDailyWorkStateDto DailyWorkStateDto { get; set; }
    }
}
