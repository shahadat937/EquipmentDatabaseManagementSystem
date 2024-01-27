using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Commands
{
    public class DeleteBaseSchoolNameCommand : IRequest  
    {  
        public int BaseSchoolNameId { get; set; }
    }
}
 