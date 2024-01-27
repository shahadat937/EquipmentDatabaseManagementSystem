using MediatR;

namespace SchoolManagement.Application.Features.ProjectStatuses.Requests.Commands
{
    public class DeleteProjectStatusCommand : IRequest
    {
        public int ProjectStatusId { get; set; }
    }
} 
