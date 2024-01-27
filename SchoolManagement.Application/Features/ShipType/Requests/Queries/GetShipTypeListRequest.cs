using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ShipInformationTypes;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ShipTypes.Requests.Queries
{
    public class GetShipTypeListRequest : IRequest<PagedResult<ShipTypeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
