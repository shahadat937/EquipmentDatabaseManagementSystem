using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.EquipmentType;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.EquipmentTypes.Requests.Queries
{
    public class GetEquipmentTypeListRequest : IRequest<PagedResult<EquipmentTypeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
