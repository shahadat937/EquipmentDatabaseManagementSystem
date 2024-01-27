using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.AppFeature.Requests.Commands
{
    public class DeleteFeatureCommand : IRequest  
    {  
        public int Id { get; set; } 
    }
}
