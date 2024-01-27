using MediatR;
using SchoolManagement.Application.DTOs.ShipInformationTypes;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ShipTypes.Requests.Commands
{
    public class CreateShipTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateShipTypeDto ShipTypeDto { get; set; }
    }
}
