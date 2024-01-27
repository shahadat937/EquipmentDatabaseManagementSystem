using MediatR;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic; 
using System.Text;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries  
{
    public class GetSelectedBaseNamesForCourseRequest : IRequest<List<SelectedModel>> 
    {
        public int BranchLevel { get; set; }
    }
}
  