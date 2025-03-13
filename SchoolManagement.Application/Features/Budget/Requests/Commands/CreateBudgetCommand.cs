using MediatR;
using SchoolManagement.Application.DTOs.Budget;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Budget.Requests.Commands
{
    public class CreateBudgetCommand : IRequest<BaseCommandResponse>
    {
        public CreateBudgetDto BudgetDto { get; set; }
    }
}
