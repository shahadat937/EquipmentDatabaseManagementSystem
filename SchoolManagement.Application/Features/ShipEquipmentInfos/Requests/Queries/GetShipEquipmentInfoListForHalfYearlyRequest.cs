using MediatR;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries
{
    public class GetShipEquipmentInfoListForHalfYearlyRequest : IRequest<List<ShipEquipmentInfoDto>>
    {
        public int EquipmentCategoryId { get; set; }
        public int EqupmentNameId { get; set; }
        public int ShipId { get; set; }
    }
}   
     