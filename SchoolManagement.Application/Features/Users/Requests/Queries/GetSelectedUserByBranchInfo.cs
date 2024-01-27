using MediatR;
using SchoolManagement.Application.DTOs.User;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Users.Requests.Queries
{
    public class GetSelectedUserByBranchInfo : IRequest<List<SelectedModel>>
    {
        public string BranchInfo { get; set; } 
    }
}
