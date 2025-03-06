using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Procurements.Requests.Queries
{
    public class GetOfferUnderEvaluationProcurementsListRequest : IRequest<PagedResult<ProcurementDto>>
    {
        public QueryParams QueryParams { get; set; }
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; }
    }
}
