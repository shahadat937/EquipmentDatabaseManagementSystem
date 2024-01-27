using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Users.Requests.Commands
{
    public class DeleteUserCommand : IRequest 
    {  
        public int Id { get; set; }
    }
}
