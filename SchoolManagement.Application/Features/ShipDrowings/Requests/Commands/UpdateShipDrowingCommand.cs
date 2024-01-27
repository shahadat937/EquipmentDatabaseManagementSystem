using MediatR;
using SchoolManagement.Application.DTOs.ShipDrowinges;
using SchoolManagement.Application.DTOs.ShipDrowings;

namespace SchoolManagement.Application.Features.ShipDrowings.Requests.Commands
{
    public class UpdateShipDrowingCommand : IRequest<Unit>
    { 
        public CreateShipDrowingDto ShipDrowingDto { get; set; }
    }
}
  