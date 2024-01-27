using MediatR;
using SchoolManagement.Application.DTOs.DealingOfficer;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.DealingOfficers.Requests.Commands
{
    public class CreateDealingOfficerCommand : IRequest<BaseCommandResponse>
    {
        public CreateDealingOfficerDto DealingOfficerDto { get; set; }
    }
}
