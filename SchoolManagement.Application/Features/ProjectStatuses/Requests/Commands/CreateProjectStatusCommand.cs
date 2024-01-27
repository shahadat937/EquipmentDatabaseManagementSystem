using MediatR;
using SchoolManagement.Application.DTOs.ProjectStatus;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ProjectStatuses.Requests.Commands
{
    public class CreateProjectStatusCommand : IRequest<BaseCommandResponse>
    {
        public CreateProjectStatusDto ProjectStatusDto { get; set; }
    }
}
