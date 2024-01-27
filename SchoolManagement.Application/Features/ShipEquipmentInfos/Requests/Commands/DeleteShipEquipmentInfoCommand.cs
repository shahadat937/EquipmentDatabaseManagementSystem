using MediatR;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Commands
{
    public class DeleteShipEquipmentInfoCommand : IRequest
    {
        public int ShipEquipmentInfoId { get; set; }
    }
} 
