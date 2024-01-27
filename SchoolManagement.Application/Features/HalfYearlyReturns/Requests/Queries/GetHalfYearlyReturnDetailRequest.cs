using MediatR;
using SchoolManagement.Application.DTOs.HalfYearlyReturn;

namespace SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Queries
{
    public class GetHalfYearlyReturnDetailRequest : IRequest<HalfYearlyReturnDto>
    {
        public int HalfYearlyReturnId { get; set; }
    }
}
