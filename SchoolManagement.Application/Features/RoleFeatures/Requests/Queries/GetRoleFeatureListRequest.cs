using MediatR;
using SchoolManagement.Application.DTOs.RoleFeature;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.RoleFeatures.Requests.Queries
{
    public class GetRoleFeatureListRequest : IRequest<PagedResult<RoleFeatureDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
