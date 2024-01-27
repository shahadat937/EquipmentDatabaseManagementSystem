using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.GroupNames;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.GroupNames.Requests.Queries
{
    public class GetGroupNameListRequest : IRequest<PagedResult<GroupNameDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
