using MediatR;
using SchoolManagement.Application.DTOs.Modules;

namespace SchoolManagement.Application.Features.Modules.Requests.Commands
{
    public class UpdateModuleCommand : IRequest<Unit>  
    { 
        public ModuleDto ModuleDto { get; set; }     
    }
}
