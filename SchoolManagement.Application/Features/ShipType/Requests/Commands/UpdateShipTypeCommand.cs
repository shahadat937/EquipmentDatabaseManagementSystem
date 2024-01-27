using MediatR;
using SchoolManagement.Application.DTOs.ShipInformationTypes;

namespace SchoolManagement.Application.Features.ShipTypes.Requests.Commands
{
    public class UpdateShipTypeCommand : IRequest<Unit>
    { 
        public ShipTypeDto ShipTypeDto { get; set; }
    }
}
 