using MediatR;
using SchoolManagement.Application.DTOs.FinancialYears;

namespace SchoolManagement.Application.Features.ReporingYears.Requests.Commands
{
    public class UpdateFinancialYearsCommand : IRequest<Unit>
    { 
        public FinancialYearsDto ReporingYearDto { get; set; }
    }
}
 