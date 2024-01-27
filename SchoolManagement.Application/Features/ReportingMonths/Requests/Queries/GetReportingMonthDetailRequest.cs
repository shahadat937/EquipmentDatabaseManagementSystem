using MediatR;
using SchoolManagement.Application.DTOs.ReportingMonths;
using SchoolManagement.Application.DTOs.ShipInformationTypes;

namespace SchoolManagement.Application.Features.ReportingMonths.Requests.Queries
{
    public class GetReportingMonthDetailRequest : IRequest<ReportingMonthDto>
    {
        public int ReportingMonthId { get; set; }
    }
}
