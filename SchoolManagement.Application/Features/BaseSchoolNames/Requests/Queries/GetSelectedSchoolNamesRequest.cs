using MediatR;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic; 
using System.Text;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries  
{
    public class GetSelectedSchoolNamesRequest : IRequest<List<SelectedModel>> 
    {
        public int ThirdLevel { get; set; }
    }
}
  