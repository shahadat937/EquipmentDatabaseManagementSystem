using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.BudgetCode;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.BudgetCode.Requests.Queries
{
    public class GetBudgetCodeListRequest : IRequest<PagedResult<BudgetCodeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
