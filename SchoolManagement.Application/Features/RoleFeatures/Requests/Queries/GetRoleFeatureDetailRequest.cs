using MediatR;
using SchoolManagement.Application.DTOs.RoleFeature;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.RoleFeatures.Requests.Queries
{
    public class GetRoleFeatureDetailRequest : IRequest<RoleFeatureDto>
    {
        public string RoleId { get; set; }
        public int FeatureId { get; set; }
    }
}
