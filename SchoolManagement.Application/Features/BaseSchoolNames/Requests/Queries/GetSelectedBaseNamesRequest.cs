using MediatR;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic; 
using System.Text;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries  
{
    public class GetSelectedBaseNamesRequest : IRequest<List<SelectedModel>> 
    {
        public int SecondLevel { get; set; }
    }
}
  