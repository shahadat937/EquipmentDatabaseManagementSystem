using MediatR;
using SchoolManagement.Application.DTOs.DailyWorkState;

namespace SchoolManagement.Application.Features.DailyWorkStates.Requests.Commands
{
    public class UpdateDailyWorkStateCommand : IRequest<Unit>
    { 
        public CreateDailyWorkStateDto UpdateDailyWorkStateDto { get; set; }
    }
}
 