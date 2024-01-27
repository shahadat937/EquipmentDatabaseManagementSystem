using MediatR;
using SchoolManagement.Application.DTOs.ShipInformationTypes;

namespace SchoolManagement.Application.Features.ShipTypes.Requests.Queries
{
    public class GetShipTypeDetailRequest : IRequest<ShipTypeDto>
    {
        public int ShipTypeId { get; set; }
    }
}
