using MediatR;

namespace SchoolManagement.Application.Features.GroupNames.Requests.Commands
{
    public class DeleteGroupNameCommand : IRequest
    {
        public int GroupNameId { get; set; }
    }
} 
