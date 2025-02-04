using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.MonthlyReturns.Requests.Queries
{
    public class GetMonthlyReturnListRequest : IRequest<PagedResult<MonthlyReturnDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int? ShipId { get; set; }
    }
}
