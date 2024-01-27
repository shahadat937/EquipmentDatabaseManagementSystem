using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.DealingOfficer;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.DealingOfficers.Requests.Queries
{
    public class GetDealingOfficerListRequest : IRequest<PagedResult<DealingOfficerDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
