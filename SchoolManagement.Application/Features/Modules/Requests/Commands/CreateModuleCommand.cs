using MediatR;
using SchoolManagement.Application.Responses;
using SchoolManagement.Application.DTOs.Modules;

namespace SchoolManagement.Application.Features.Modules.Requests.Commands
{
    public class CreateModuleCommand : IRequest<BaseCommandResponse> 
    {
        public CreateModuleDto ModuleDto { get; set; }      

    }
}
