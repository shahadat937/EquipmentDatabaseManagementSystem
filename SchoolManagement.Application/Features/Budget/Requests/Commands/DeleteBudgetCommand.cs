using MediatR;

namespace SchoolManagement.Application.Features.Budget.Requests.Commands
{
    public class DeleteBudgetCommand : IRequest
    {
        public int BudgetId { get; set; }
    }
} 
