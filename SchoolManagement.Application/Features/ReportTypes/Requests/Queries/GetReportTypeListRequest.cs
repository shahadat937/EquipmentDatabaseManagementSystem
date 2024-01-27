using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ReportType;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ReportTypes.Requests.Queries
{
    public class GetReportTypeListRequest : IRequest<PagedResult<ReportTypeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
