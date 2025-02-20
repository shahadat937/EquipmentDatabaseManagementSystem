using MediatR;
using SchoolManagement.Application.DTOs.ReporingYear;
using SchoolManagement.Application.DTOs.ReportingYear;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.YearSetups.Requests.Commands
{
    public class CreateReportingYearCommand : IRequest<BaseCommandResponse>
    {
        public CreateReportingYearDto ReportingYearDto { get; set; }
    }
}
