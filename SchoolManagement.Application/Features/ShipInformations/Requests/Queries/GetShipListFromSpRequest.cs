using MediatR;

namespace SchoolManagement.Application.Features.ShipInformations.Requests.Queries
{
    public class GetShipListFromSpRequest : IRequest<object>
    {
        public int ShipTypeId { get; set; }
    }
}
  