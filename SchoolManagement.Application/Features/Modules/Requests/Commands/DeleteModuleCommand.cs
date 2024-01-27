using MediatR;

namespace SchoolManagement.Application.Features.Modules.Requests.Commands
{
    public class DeleteModuleCommand : IRequest  
    {  
        public int Id { get; set; }
    }
}
