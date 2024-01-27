using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class Feature
    {
        public int FeatureId { get; set; }
        public int ModuleId { get; set; }
        public int FeatureTypeId { get; set; }
        public string FeatureName { get; set; }
        public string Path { get; set; }
        public string IconName { get; set; }
        public string Icon { get; set; }
        public string Class { get; set; }
        public string GroupTitle { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public int FeatureCode { get; set; }
        public int OrderNo { get; set; }
        public bool IsReport { get; set; }

        public virtual Module Module { get; set; }
    }
}
