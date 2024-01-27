using MediatR;
using SchoolManagement.Application.DTOs.ReportingMonths;
using SchoolManagement.Application.DTOs.ShipInformationTypes;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ReportingMonths.Requests.Commands
{
    public class CreateReportingMonthCommand : IRequest<BaseCommandResponse>
    {
        public CreateReportingMonthDto ReportingMonthDto { get; set; }
    }
}
