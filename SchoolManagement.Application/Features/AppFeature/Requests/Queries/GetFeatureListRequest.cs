using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Features;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.AppFeature.Requests.Queries
{
    public class GetFeatureListRequest : IRequest<PagedResult<FeatureDto>>
    { 
        public QueryParams QueryParams { get; set; }  
    }
}
