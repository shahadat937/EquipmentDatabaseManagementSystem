using MediatR;

namespace SchoolManagement.Application.Features.ShipInformations.Requests.Queries
{
    public class GetShipInformationByBaseSchoolNameRequest : IRequest<object>
    {
        public int BaseSchoolNameId { get; set; }
    }
}
    