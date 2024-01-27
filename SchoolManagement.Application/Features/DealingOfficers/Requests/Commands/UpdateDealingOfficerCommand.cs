using MediatR;
using SchoolManagement.Application.DTOs.DealingOfficer;

namespace SchoolManagement.Application.Features.DealingOfficers.Requests.Commands
{
    public class UpdateDealingOfficerCommand : IRequest<Unit>
    { 
        public DealingOfficerDto DealingOfficerDto { get; set; }
    }
}
 