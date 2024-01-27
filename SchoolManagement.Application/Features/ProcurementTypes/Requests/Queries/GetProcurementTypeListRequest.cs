using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ProcurementType;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ProcurementTypes.Requests.Queries
{
    public class GetProcurementTypeListRequest : IRequest<PagedResult<ProcurementTypeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
