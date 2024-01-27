using MediatR;
using SchoolManagement.Application.DTOs.ReportType;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ReportTypes.Requests.Commands
{
    public class CreateReportTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateReportTypeDto ReportTypeDto { get; set; }
    }
}
