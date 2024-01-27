using MediatR;
using SchoolManagement.Application.DTOs.GroupNames;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.GroupNames.Requests.Commands
{
    public class CreateGroupNameCommand : IRequest<BaseCommandResponse>
    {
        public CreateGroupNameDto GroupNameDto { get; set; }
    }
}
