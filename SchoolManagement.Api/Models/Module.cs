using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class Module
    {
        public Module()
        {
            Features = new HashSet<Feature>();
        }

        public int ModuleId { get; set; }
        public string Title { get; set; }
        public string ModuleName { get; set; }
        public string IconName { get; set; }
        public string Icon { get; set; }
        public string Class { get; set; }
        public string GroupTitle { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Feature> Features { get; set; }
    }
}
