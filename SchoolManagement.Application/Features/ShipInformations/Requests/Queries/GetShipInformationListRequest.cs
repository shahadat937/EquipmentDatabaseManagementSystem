using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ShipInformations;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ShipInformations.Requests.Queries
{
    public class GetShipInformationListRequest : IRequest<PagedResult<ShipInformationDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int? ShipId { get; set; }
    }
}
