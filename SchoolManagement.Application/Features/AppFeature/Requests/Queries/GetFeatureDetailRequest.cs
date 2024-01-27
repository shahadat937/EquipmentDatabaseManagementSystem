using MediatR;
using SchoolManagement.Application.DTOs.Features;

namespace SchoolManagement.Application.Features.AppFeature.Requests.Queries
{
    public class GetFeatureDetailRequest : IRequest<FeatureDto> 
    {
        public int Id { get; set; } 
    }
}
