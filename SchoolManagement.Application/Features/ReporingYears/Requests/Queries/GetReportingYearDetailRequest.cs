using MediatR;
using SchoolManagement.Application.DTOs.ReportingYear;

namespace SchoolManagement.Application.Features.YearSetups.Requests.Queries
{
    public class GetReportingYearDetailRequest : IRequest<ReportingYearDto>
    {
        public int ReportingYearId { get; set; }
    }
}
