using MediatR;
using SchoolManagement.Application.DTOs.Features;

namespace SchoolManagement.Application.Features.AppFeature.Requests.Commands
{
    public class UpdateFeatureCommand : IRequest<Unit>  
    { 
        public FeatureDto FeatureDto { get; set; }      
    }
}
