using MediatR;
using SchoolManagement.Application.DTOs.Branch;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.User;
using SchoolManagement.Application.Models;
using System; 
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Users.Requests.Queries 
{ 
    public class GetUsersListRequest : IRequest<PagedResult<UserDto>>
    {
        public QueryParams QueryParams { get; set; }  
    }
}
