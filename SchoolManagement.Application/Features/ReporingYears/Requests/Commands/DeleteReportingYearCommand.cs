using MediatR;

namespace SchoolManagement.Application.Features.YearSetups.Requests.Commands
{
    public class DeleteReportingYearCommand : IRequest
    {
        public int ReportingYearId { get; set; }
    }
} 
