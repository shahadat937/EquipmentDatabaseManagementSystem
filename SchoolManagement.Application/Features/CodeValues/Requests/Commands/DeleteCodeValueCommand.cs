using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.CodeValues.Requests.Commands
{
    public class DeleteCodeValueCommand : IRequest  
    {  
        public int Id { get; set; }
    }
}
