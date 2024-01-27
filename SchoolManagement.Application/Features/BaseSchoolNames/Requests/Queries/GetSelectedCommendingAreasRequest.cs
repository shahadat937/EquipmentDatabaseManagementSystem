using MediatR;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic; 
using System.Text;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries  
{
    public class GetSelectedCommendingAreasRequest : IRequest<List<SelectedModel>> 
    {
        public int FirstLevel { get; set; }
    }
}
  