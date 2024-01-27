using MediatR;
using SchoolManagement.Application.DTOs.Priority;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Prioritys.Requests.Commands
{
    public class CreatePriorityCommand : IRequest<BaseCommandResponse>
    {
        public CreatePriorityDto PriorityDto { get; set; }
    }
}
