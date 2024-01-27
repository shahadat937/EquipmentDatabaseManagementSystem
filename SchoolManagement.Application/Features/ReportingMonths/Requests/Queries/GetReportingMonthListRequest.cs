using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ReportingMonths;
using SchoolManagement.Application.DTOs.ShipInformationTypes;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ReportingMonths.Requests.Queries
{
    public class GetReportingMonthListRequest : IRequest<PagedResult<ReportingMonthDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
