using MediatR;
using SchoolManagement.Application.DTOs.ShipInformations;

namespace SchoolManagement.Application.Features.ShipInformations.Requests.Commands
{
    public class UpdateShipInformationCommand : IRequest<Unit>
    { 
        public ShipInformationDto ShipInformationDto { get; set; }
    }
}
 