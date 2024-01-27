using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class RoleFeature
    {
        public string RoleId { get; set; }
        public int FeatureKey { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Report { get; set; }
    }
}
