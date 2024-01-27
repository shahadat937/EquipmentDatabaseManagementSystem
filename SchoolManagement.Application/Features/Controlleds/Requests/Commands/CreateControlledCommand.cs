using MediatR;
using SchoolManagement.Application.DTOs.Controlled;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Controlleds.Requests.Commands
{
    public class CreateControlledCommand : IRequest<BaseCommandResponse>
    {
        public CreateControlledDto ControlledDto { get; set; }
    }
}
