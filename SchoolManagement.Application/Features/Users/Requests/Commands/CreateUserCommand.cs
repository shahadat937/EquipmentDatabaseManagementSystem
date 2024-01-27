using MediatR;
using SchoolManagement.Application.DTOs.User;
using SchoolManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Users.Requests.Commands 
{
    public class CreateUsersCommand : IRequest<BaseCommandResponse> 
    {
        public CreateUserDto UserDto { get; set; }      

    }
}
