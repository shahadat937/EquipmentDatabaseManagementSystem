using MediatR;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;

namespace SchoolManagement.Application.Features.Users.Requests.Queries
{
    public class GetSelectedUserByRole : IRequest<List<SelectedModel>>
    {
        public string Role { get; set; } 
    }
}
