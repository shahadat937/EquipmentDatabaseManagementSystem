using MediatR;

namespace SchoolManagement.Application.Features.ReportTypes.Requests.Commands
{
    public class DeleteReportTypeCommand : IRequest
    {
        public int ReportTypeId { get; set; }
    }
} 
