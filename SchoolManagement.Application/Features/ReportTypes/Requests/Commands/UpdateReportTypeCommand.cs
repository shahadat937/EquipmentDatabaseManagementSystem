using MediatR;
using SchoolManagement.Application.DTOs.ReportType;

namespace SchoolManagement.Application.Features.ReportTypes.Requests.Commands
{
    public class UpdateReportTypeCommand : IRequest<Unit>
    { 
        public ReportTypeDto ReportTypeDto { get; set; }
    }
}
 