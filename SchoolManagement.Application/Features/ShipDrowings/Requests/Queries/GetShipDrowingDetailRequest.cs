using MediatR;
using SchoolManagement.Application.DTOs.ShipDrowings;

namespace SchoolManagement.Application.Features.ShipDrowings.Requests.Queries
{
    public class GetShipDrowingDetailRequest : IRequest<ShipDrowingDto>
    {
        public int ShipDrowingId { get; set; }
    }
}
