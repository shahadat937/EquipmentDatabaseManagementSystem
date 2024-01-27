using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.EquipmentCategorys;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.EquipmentCategorys.Requests.Queries
{
    public class GetEquipmentCategoryListRequest : IRequest<PagedResult<EquipmentCategoryDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
