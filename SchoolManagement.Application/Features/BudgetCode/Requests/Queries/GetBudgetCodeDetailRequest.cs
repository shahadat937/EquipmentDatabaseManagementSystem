using MediatR;
using SchoolManagement.Application.DTOs.BudgetCode;

namespace SchoolManagement.Application.Features.BudgetCode.Requests.Queries
{
    public class GetBudgetCodeDetailRequest : IRequest<BudgetCodeDto>
    {
        public int BudgetCodeId { get; set; }
    }
}
