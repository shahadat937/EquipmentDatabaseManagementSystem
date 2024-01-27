using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.StateOfEquipments;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.StateOfEquipments.Requests.Queries
{
    public class GetStateOfEquipmentListRequest : IRequest<PagedResult<StateOfEquipmentDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
