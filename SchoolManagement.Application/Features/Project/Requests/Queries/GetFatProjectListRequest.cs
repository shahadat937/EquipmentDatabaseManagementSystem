using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Project;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Project.Requests.Queries
{
    public class GetFatProjectListRequest : IRequest<PagedResult<ProjectDto>>
    {
        public QueryParams QueryParams { get; set; }
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; }
    }
}
