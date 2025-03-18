using MediatR;
using SchoolManagement.Application.DTOs.Project;

namespace SchoolManagement.Application.Features.Project.Requests.Commands
{
    public class UpdateProjectCommand : IRequest<Unit>
    { 
        public CreateProjectDto ProjectDto { get; set; }
    }
}
 