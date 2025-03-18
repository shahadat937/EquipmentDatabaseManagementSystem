using MediatR;
using SchoolManagement.Application.DTOs.Project;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Project.Requests.Commands
{
    public class CreateProjectCommand : IRequest<BaseCommandResponse>
    {
        public CreateProjectDto ProjectDto { get; set; }
    }
}
