using MediatR;
using SchoolManagement.Application.DTOs.ReportingYear;

namespace SchoolManagement.Application.Features.ReporingYears.Requests.Commands
{
    public class UpdateReportingYearCommand : IRequest<Unit>
    { 
        public ReportingYearDto ReporingYearDto { get; set; }
    }
}
 