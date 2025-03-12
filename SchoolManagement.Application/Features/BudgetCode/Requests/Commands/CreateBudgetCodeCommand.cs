using MediatR;
using SchoolManagement.Application.DTOs.BudgetCode;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.BudgetCode.Requests.Commands
{
    public class CreateBudgetCodeCommand : IRequest<BaseCommandResponse>
    {
        public CreateBudgetCodeDto BudgetCodeDto { get; set; }
    }
}
