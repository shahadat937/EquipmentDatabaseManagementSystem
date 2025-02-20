using MediatR;
using SchoolManagement.Application.DTOs.YearSetup;

namespace SchoolManagement.Application.Features.YearSetups.Requests.Commands
{
    public class UpdateYearSetupCommand : IRequest<Unit>
    { 
        public YearSetupDto YearSetupDto { get; set; }
    }
}
 