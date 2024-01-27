using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ProjectStatus;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ProjectStatuses.Requests.Queries
{
    public class GetProjectStatusListRequest : IRequest<PagedResult<ProjectStatusDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
