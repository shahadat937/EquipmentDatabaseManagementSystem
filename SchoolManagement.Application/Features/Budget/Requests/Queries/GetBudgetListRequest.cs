using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Budget;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Budget.Requests.Queries
{
    public class GetBudgetListRequest : IRequest<PagedResult<BudgetDto>>
    {
        public QueryParams QueryParams { get; set; }
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; }
    }
}
