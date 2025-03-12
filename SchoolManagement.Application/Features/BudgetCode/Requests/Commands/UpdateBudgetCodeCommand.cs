using MediatR;
using SchoolManagement.Application.DTOs.BudgetCode;
using SchoolManagement.Application.DTOs.FinancialYears;

namespace SchoolManagement.Application.Features.BudgetCode.Requests.Commands
{
    public class UpdateBudgetCodeCommand : IRequest<Unit>
    { 
        public BudgetCodeDto BudgetCode { get; set; }
    }
}
 