using MediatR;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;

namespace SchoolManagement.Application.Features.AppFeature.Requests.Queries
{
    public class GetSelectedFeatureByTypeRequest : IRequest<List<SelectedModel>>
    {
        public string Type { get; set; }    
    }
}
