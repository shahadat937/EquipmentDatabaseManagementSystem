using MediatR;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.AppFeature.Requests.Queries
{
    public class GetSelectedFeatureRequest : IRequest<List<SelectedModel>>
    {
    }
}
