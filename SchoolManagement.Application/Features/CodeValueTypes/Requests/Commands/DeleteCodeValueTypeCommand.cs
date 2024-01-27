using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.CodeValueTypes.Requests.Commands
{
    public class DeleteCodeValueTypeCommand : IRequest 
    {  
        public int CodeValueTypeId { get; set; }
    }
}
