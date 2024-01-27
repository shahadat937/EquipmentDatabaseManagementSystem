using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Modules;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Modules.Requests.Queries
{
    public class GetModuleListRequest : IRequest<PagedResult<ModuleDto>>
    { 
        public QueryParams QueryParams { get; set; }  
    }
}
