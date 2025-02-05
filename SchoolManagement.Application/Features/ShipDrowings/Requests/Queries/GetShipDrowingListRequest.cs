using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ShipDrowings;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ShipDrowings.Requests.Queries
{
    public class GetShipDrowingListRequest : IRequest<PagedResult<ShipDrowingDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int? ShipId { get; set; }
        /// public int? DepartmentNameId { get; set; }
    }
}
