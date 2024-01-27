using MediatR;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Commands
{
    public class CreateShipEquipmentInfoCommand : IRequest<BaseCommandResponse>
    {
        public CreateShipEquipmentInfoDto ShipEquipmentInfoDto { get; set; }
    }
}
