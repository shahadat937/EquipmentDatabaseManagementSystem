using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public class RunningTime : BaseDomainEntity
    {
        //public RunningTime()
        //{
        //    ShipInformations = new HashSet<ShipInformation>();
        //}
        public int RunningTimeId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; } 
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        //public virtual ICollection<ShipInformation> ShipInformations { get; set; }
    }
}
