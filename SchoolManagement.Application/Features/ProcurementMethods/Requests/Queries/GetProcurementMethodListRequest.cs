using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ProcurementMethod;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ProcurementMethods.Requests.Queries
{
    public class GetProcurementMethodListRequest : IRequest<PagedResult<ProcurementMethodDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
