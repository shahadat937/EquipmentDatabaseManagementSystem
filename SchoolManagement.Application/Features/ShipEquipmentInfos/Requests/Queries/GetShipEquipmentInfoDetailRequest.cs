using MediatR;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries
{
    public class GetShipEquipmentInfoDetailRequest : IRequest<ShipEquipmentInfoDto>
    {
        public int ShipEquipmentInfoId { get; set; }
    }
}
