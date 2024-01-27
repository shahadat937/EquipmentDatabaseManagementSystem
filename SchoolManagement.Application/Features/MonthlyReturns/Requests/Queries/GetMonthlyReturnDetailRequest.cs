using MediatR;
using SchoolManagement.Application.DTOs.MonthlyReturns;

namespace SchoolManagement.Application.Features.MonthlyReturns.Requests.Queries
{
    public class GetMonthlyReturnDetailRequest : IRequest<MonthlyReturnDto>
    {
        public int MonthlyReturnId { get; set; }
    }
}
