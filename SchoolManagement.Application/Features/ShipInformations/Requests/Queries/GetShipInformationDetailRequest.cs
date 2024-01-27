using MediatR;
using SchoolManagement.Application.DTOs.ShipInformations;

namespace SchoolManagement.Application.Features.ShipInformations.Requests.Queries
{
    public class GetShipInformationDetailRequest : IRequest<ShipInformationDto>
    {
        public int ShipInformationId { get; set; }
    }
}
