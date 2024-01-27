using MediatR;

namespace SchoolManagement.Application.Features.MonthlyReturns.Requests.Commands
{
    public class DeleteMonthlyReturnCommand : IRequest
    {
        public int MonthlyReturnId { get; set; }
    }
} 
  