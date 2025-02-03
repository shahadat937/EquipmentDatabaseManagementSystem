using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries
{
    public class GetSelectedShipEquipmentByShipIdEquipmentNameIdInfoRequest : IRequest<List<SelectedModel>>
    {
        public int? BaseSchoolNameId { get; set; }
        public int? EquipmentNameId { get; set; }
    }
} 
