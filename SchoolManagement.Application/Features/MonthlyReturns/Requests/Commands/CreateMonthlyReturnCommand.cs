using MediatR;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.MonthlyReturns.Requests.Commands
{
    public class CreateMonthlyReturnCommand : IRequest<BaseCommandResponse>
    {
        public CreateMonthlyReturnDto MonthlyReturnDto { get; set; }
    }
}
