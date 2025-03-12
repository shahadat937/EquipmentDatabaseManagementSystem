using MediatR;

namespace SchoolManagement.Application.Features.BudgetCode.Requests.Commands
{
    public class DeleteBudgetCodeCommand : IRequest
    {
        public int BudgetCodeId { get; set; }
    }
} 
