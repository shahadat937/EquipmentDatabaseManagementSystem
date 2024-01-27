using MediatR;
using SchoolManagement.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Users.Requests.Commands
{
    public class UpdateUserCommand : IRequest<Unit>  
    { 
        public UserDto UserDto { get; set; }     
    }
}
