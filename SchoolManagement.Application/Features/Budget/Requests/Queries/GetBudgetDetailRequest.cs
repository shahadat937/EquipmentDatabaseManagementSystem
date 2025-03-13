using MediatR;
using SchoolManagement.Application.DTOs.Budget;

namespace SchoolManagement.Application.Features.Budget.Requests.Queries
{
    public class GetBudgetDetailRequest : IRequest<BudgetDto>
    {
        public int BudgetId { get; set; }
    }
}
