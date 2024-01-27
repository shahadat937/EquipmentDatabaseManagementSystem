using MediatR;
using SchoolManagement.Application.DTOs.Modules;

namespace SchoolManagement.Application.Features.Modules.Requests.Queries
{
    public class GetModuleDetailRequest : IRequest<ModuleDto> 
    {
        public int Id { get; set; } 
    }
}
