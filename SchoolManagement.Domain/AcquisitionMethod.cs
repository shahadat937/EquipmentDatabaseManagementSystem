using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class AcquisitionMethod : BaseDomainEntity
    {
        public AcquisitionMethod()
        {
            ShipEquipmentInfos = new HashSet<ShipEquipmentInfo>();
        }
        public int AcquisitionMethodId { get; set; } 
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ShipEquipmentInfo> ShipEquipmentInfos { get; set; }
    }
}
