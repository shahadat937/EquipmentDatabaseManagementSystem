using MediatR;

namespace SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Commands
{
    public class DeleteHalfYearlyReturnCommand : IRequest
    {
        public int HalfYearlyReturnId { get; set; }
    }
} 
