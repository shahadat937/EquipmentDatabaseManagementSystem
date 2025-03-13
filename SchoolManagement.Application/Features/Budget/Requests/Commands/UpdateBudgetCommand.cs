using MediatR;
using SchoolManagement.Application.DTOs.Budget;

namespace SchoolManagement.Application.Features.Budget.Requests.Commands
{
    public class UpdateBudgetCommand : IRequest<Unit>
    { 
        public CreateBudgetDto BudgetDto { get; set; }
    }
}
 