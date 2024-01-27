using MediatR;
using SchoolManagement.Application.DTOs.ShipInformations;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ShipInformations.Requests.Commands
{
    public class CreateShipInformationCommand : IRequest<BaseCommandResponse>
    {
        public CreateShipInformationDto ShipInformationDto { get; set; }
    }
}
