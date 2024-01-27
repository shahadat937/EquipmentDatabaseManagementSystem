using MediatR;
using SchoolManagement.Application.DTOs.ProjectStatus;

namespace SchoolManagement.Application.Features.ProjectStatuses.Requests.Commands
{
    public class UpdateProjectStatusCommand : IRequest<Unit>
    { 
        public ProjectStatusDto ProjectStatusDto { get; set; }
    }
}
 