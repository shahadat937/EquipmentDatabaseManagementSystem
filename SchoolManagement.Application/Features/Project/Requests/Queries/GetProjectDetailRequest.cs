using MediatR;
using SchoolManagement.Application.DTOs.Project;

namespace SchoolManagement.Application.Features.Project.Requests.Queries
{
    public class GetProjectDetailRequest : IRequest<ProjectDto>
    {
        public int ProjectId { get; set; }
    }
}
