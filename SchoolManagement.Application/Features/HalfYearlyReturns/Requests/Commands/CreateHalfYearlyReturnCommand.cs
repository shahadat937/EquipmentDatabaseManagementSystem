using MediatR;
using SchoolManagement.Application.DTOs.HalfYearlyReturn;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Commands
{
    public class CreateHalfYearlyReturnCommand : IRequest<BaseCommandResponse>
    {
        public List<CreateHalfYearlyReturnDto> HalfYearlyReturnDto { get; set; }
    }
}
