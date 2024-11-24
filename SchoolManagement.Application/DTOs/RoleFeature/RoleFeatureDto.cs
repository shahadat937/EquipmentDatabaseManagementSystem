using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.RoleFeature
{
    public class RoleFeatureDto : IRoleFeatureDto
    {

        public string RoleId { get; set; }
        public int FeatureKey { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Report { get; set; }
        public bool IsActive { get; set; }
        public string? RoleName { get; set; }
        public string? FeatureName { get; set; }
    }
}

