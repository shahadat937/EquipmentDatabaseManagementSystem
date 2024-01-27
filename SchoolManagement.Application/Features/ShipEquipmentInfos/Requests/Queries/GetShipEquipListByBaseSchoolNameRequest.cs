using MediatR;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries
{
    public class GetShipEquipListByBaseSchoolNameRequest : IRequest<object>
    {
        public int BaseSchoolNameId { get; set; }
    }
}
    