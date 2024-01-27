using SchoolManagement.Doamin;
using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class EqupmentName :BaseDomainEntity
    {
        public EqupmentName()
        {
            ShipEquipmentInfos = new HashSet<ShipEquipmentInfo>();
            Procurements = new HashSet<Procurement>();
            MonthlyReturns = new HashSet<MonthlyReturn>();
            OperationalStates = new HashSet<OperationalState>();
            HalfYearlyReturns = new HashSet<HalfYearlyReturn>();
        }

        public int EqupmentNameId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int? EquipmentCategoryId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName? BaseSchoolName { get; set; }
        public virtual EquipmentCategory? EquipmentCategory { get; set; }
        public virtual ICollection<ShipEquipmentInfo> ShipEquipmentInfos { get; set; }
        public virtual ICollection<Procurement> Procurements { get; set; }
        public virtual ICollection<MonthlyReturn> MonthlyReturns { get; set; }
        public virtual ICollection<HalfYearlyReturn> HalfYearlyReturns { get; set; }
        public virtual ICollection<OperationalState> OperationalStates { get; set; }
    }
}
