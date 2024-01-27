using MediatR;
using SchoolManagement.Application.DTOs.ProjectStatus;

namespace SchoolManagement.Application.Features.ProjectStatuses.Requests.Queries
{
    public class GetProjectStatusDetailRequest : IRequest<ProjectStatusDto>
    {
        public int ProjectStatusId { get; set; }
    }
}
