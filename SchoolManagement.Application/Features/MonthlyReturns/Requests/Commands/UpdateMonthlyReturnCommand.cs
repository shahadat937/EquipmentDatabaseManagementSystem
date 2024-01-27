using MediatR;
using SchoolManagement.Application.DTOs.MonthlyReturns;

namespace SchoolManagement.Application.Features.MonthlyReturns.Requests.Commands
{
    public class UpdateMonthlyReturnCommand : IRequest<Unit>
    { 
        public MonthlyReturnDto MonthlyReturnDto { get; set; }
    }
}
 