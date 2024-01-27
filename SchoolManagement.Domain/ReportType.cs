using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public class ReportType : BaseDomainEntity
    {
        //public ReportType()
        //{
        //    ShipInformations = new HashSet<ShipInformation>();
        //}
        public int ReportTypeId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; } 
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        //public virtual ICollection<ShipInformation> ShipInformations { get; set; }
    }
}
