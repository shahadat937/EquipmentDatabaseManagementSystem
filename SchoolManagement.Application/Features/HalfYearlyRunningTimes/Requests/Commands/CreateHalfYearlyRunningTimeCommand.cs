using MediatR;
using SchoolManagement.Application.DTOs.HalfYearlyRunningTime;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.HalfYearlyRunningTimes.Requests.Commands
{
    public class CreateHalfYearlyRunningTimeCommand : IRequest<BaseCommandResponse>
    {
        public CreateHalfYearlyRunningTimeDto HalfYearlyRunningTimeDto { get; set; }
    }
}
