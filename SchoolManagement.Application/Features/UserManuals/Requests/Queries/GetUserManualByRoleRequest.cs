using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.UserManuals.Requests.Queries
{
    public class GetUserManualByRoleRequest:IRequest<object>
    {
        public string RoleName { get; set; }
    }
}
