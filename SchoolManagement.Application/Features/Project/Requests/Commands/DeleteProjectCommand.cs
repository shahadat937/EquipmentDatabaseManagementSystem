using MediatR;

namespace SchoolManagement.Application.Features.Project.Requests.Commands
{
    public class DeleteProjectCommand : IRequest
    {
        public int ProjectId { get; set; }
    }
} 
