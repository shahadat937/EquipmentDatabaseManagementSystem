using MediatR;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;

namespace SchoolManagement.Application.Features.Modules.Requests.Queries
{
    public class GetSelectedModuleRequests : IRequest<List<SelectedModel>>
    {
        public string Type { get; set; }  
    } 
}
