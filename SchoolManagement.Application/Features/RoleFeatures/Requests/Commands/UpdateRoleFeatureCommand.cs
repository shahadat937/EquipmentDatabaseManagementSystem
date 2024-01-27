using MediatR;
using SchoolManagement.Application.DTOs.RoleFeature;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.RoleFeatures.Requests.Commands
{
    public class UpdateRoleFeatureCommand : IRequest<Unit>
    {
        public RoleFeatureDto RoleFeatureDto { get; set; }
        public string RoleId { get; set; }
        public int FeatureId { get; set; }
    }
}
