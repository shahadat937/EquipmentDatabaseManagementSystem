using MediatR;
using SchoolManagement.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.Features.Users.Requests.Queries
{
    public class GetUsersDetailRequest : IRequest<UserDto> 
    {
        public int Id { get; set; } 
    }
}
