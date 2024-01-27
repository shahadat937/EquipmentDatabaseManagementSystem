using MediatR;
using SchoolManagement.Application.DTOs.ReportingMonths;
using SchoolManagement.Application.DTOs.ShipInformationTypes;

namespace SchoolManagement.Application.Features.ReportingMonths.Requests.Commands
{
    public class UpdateReportingMonthCommand : IRequest<Unit>
    { 
        public ReportingMonthDto ReportingMonthDto { get; set; }
    }
}
 