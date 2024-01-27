using MediatR;

namespace SchoolManagement.Application.Features.ReportingMonths.Requests.Commands
{
    public class DeleteReportingMonthCommand : IRequest
    {
        public int ReportingMonthId { get; set; }
    }
} 
