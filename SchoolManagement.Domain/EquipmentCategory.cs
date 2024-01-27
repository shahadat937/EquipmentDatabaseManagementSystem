using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class EquipmentCategory:BaseDomainEntity
    {
        public EquipmentCategory()
        {
            EqupmentNames = new HashSet<EqupmentName>();
            ShipEquipmentInfos = new HashSet<ShipEquipmentInfo>();
            MonthlyReturns = new HashSet<MonthlyReturn>();
            HalfYearlyReturns = new HashSet<HalfYearlyReturn>();
        }

        public int EquipmentCategoryId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int? GroupNameId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName? BaseSchoolName { get; set; }
        public virtual GroupName? GroupName { get; set; }
        public virtual ICollection<EqupmentName> EqupmentNames { get; set; }
        public virtual ICollection<HalfYearlyReturn> HalfYearlyReturns { get; set; }
        public virtual ICollection<ShipEquipmentInfo> ShipEquipmentInfos { get; set; }
        public virtual ICollection<MonthlyReturn> MonthlyReturns { get; set; }
    }
}
