using MediatR;
using SchoolManagement.Application.DTOs.ReportType;

namespace SchoolManagement.Application.Features.ReportTypes.Requests.Queries
{
    public class GetReportTypeDetailRequest : IRequest<ReportTypeDto>
    {
        public int ReportTypeId { get; set; }
    }
}
