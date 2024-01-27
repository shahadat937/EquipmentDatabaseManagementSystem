using MediatR;
using SchoolManagement.Application.Responses;
using SchoolManagement.Application.DTOs.Features;

namespace SchoolManagement.Application.Features.AppFeature.Requests.Commands
{
    public class CreateFeatureCommand : IRequest<BaseCommandResponse> 
    { 
        public CreateFeatureDto FeatureDto { get; set; }      

    }
}
