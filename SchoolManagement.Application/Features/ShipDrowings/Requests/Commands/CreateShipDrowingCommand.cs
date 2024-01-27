using MediatR;
using SchoolManagement.Application.DTOs.ShipDrowinges;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ShipDrowings.Requests.Commands
{
    public class CreateShipDrowingCommand : IRequest<BaseCommandResponse>
    {
        public CreateShipDrowingDto ShipDrowingDto { get; set; }
    }
}
