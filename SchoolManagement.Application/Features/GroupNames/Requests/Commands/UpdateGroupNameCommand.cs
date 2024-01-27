using MediatR;
using SchoolManagement.Application.DTOs.GroupNames;

namespace SchoolManagement.Application.Features.GroupNames.Requests.Commands
{
    public class UpdateGroupNameCommand : IRequest<Unit>
    { 
        public GroupNameDto GroupNameDto { get; set; }
    }
}
 