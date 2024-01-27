using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public class RoleFeature 
    {
        public string RoleId { get; set; }
        public int FeatureKey { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Report { get; set; }
      
    }
}
