using MediatR;

namespace SchoolManagement.Application.Features.YearSetups.Requests.Commands
{
    public class DeleteFinancialYearsCommand : IRequest
    {
        public int FinancialYearId { get; set; }
    }
} 
