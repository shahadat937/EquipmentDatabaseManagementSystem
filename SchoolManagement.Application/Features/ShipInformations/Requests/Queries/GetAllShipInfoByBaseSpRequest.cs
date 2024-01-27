using MediatR;

namespace SchoolManagement.Application.Features.ShipInformations.Requests.Queries
{
    public class GetAllShipInfoByBaseSpRequest : IRequest<object>
    {
      //  public int BaseNameId { get; set; }
        public int AuthorityId { get; set; }
        public int OperationalStatusId { get; set; }
    }
}
    