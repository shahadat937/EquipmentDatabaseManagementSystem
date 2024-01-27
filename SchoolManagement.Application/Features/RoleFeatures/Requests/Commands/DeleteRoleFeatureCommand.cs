using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.RoleFeatures.Requests.Commands
{
    public class DeleteRoleFeatureCommand : IRequest
    {
        public string RoleId { get; set; }
        public int FeatureId { get; set; }
    }
}
