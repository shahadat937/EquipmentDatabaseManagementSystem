using MediatR;
using SchoolManagement.Application.DTOs.UserManual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.UserManuals.Requests.Commands
{
    public class UpdateUserManualCommand : IRequest<Unit>
    {
        public CreateUserManualDto CreateUserManualDto { get; set; }
    }
}
