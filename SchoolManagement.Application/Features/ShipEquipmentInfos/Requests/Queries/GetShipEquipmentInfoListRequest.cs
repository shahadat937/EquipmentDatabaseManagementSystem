using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries
{
    public class GetShipEquipmentInfoListRequest : IRequest<PagedResult<ShipEquipmentInfoDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int ShipId { get; set; }
    }
}
