using MediatR;
using SchoolManagement.Application.DTOs.Modules;

namespace SchoolManagement.Application.Features.Modules.Requests.Queries
{
    public class GetModuleFeaturesRequests : IRequest<List<ModuleFeatureDto>>
    {
        public int FeatureType { get; set; }  
    } 
}
