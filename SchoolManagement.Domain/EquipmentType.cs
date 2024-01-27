using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public class EquipmentType : BaseDomainEntity
    {
        public EquipmentType()
        {
            ShipEquipmentInfos = new HashSet<ShipEquipmentInfo>();
        }
        public int EquipmentTypeId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; } 
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ShipEquipmentInfo> ShipEquipmentInfos { get; set; }
    }
}
