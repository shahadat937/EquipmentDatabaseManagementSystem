using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ReportingYear;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.YearSetups.Requests.Queries
{
    public class GetReportingYearListRequest : IRequest<PagedResult<ReportingYearDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
