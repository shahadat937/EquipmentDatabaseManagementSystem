using MediatR;
using SchoolManagement.Application.DTOs.HalfYearlyReturn;

namespace SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Commands
{
    public class UpdateHalfYearlyReturnCommand : IRequest<Unit>
    { 
        public HalfYearlyReturnDto HalfYearlyReturnDto { get; set; }
    }
}
 