using MediatR;
using SchoolManagement.Application.DTOs.BaseSchoolNames;
using SchoolManagement.Application.DTOs.ShipInformations;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries
{
    public class GetShipInformationListByShipTypeIdRequest : IRequest<List<ShipInformationDto>>
    {
        public int ShipTypeId { get; set; }
    }
}

  