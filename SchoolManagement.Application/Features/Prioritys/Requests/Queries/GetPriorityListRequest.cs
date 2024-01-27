using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Priority;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Prioritys.Requests.Queries
{
    public class GetPriorityListRequest : IRequest<PagedResult<PriorityDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
