using MediatR;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Commands
{
    public class UpdateShipEquipmentInfoCommand : IRequest<Unit>
    { 
        public ShipEquipmentInfoDto ShipEquipmentInfoDto { get; set; }
    }
}
 