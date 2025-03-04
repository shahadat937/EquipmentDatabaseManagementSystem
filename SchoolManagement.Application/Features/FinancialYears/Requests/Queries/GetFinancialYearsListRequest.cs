using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.FinancialYears;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.YearSetups.Requests.Queries
{
    public class GetFinancialYearsListRequest : IRequest<PagedResult<FinancialYearDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
