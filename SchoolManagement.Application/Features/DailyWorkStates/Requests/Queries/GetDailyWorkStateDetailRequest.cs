using MediatR;
using SchoolManagement.Application.DTOs.DailyWorkState;

namespace SchoolManagement.Application.Features.DailyWorkStates.Requests.Queries
{
    public class GetDailyWorkStateDetailRequest : IRequest<DailyWorkStateDto>
    {
        public int DailyWorkStateId { get; set; }
    }
}
