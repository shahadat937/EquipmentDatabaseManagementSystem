using MediatR;
using SchoolManagement.Application.DTOs.RoleFeature;
using SchoolManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.RoleFeatures.Requests.Commands
{
    public class CreateRoleFeatureCommand : IRequest<BaseCommandResponse>
    {
        public CreateRoleFeatureDto RoleFeatureDto { get; set; } 

    }
}
