using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.RoleFeature 
{
    public class CreateRoleFeatureDto : IRoleFeatureDto
    {
        public int RoleId { get; set; }
        public int FeatureId { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Report { get; set; }
        public bool IsActive { get; set; }
    }
}
