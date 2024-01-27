using MediatR;
using SchoolManagement.Application.DTOs.ActionTaken;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ActionTakens.Requests.Commands
{
    public class CreateActionTakenCommand : IRequest<BaseCommandResponse>
    {
        public CreateActionTakenDto ActionTakenDto { get; set; }
    }
}
