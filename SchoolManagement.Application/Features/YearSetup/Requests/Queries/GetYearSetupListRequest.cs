using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.YearSetup;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.YearSetups.Requests.Queries
{
    public class GetYearSetupListRequest : IRequest<PagedResult<YearSetupDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
