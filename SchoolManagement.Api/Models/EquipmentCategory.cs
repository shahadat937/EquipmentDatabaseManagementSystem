using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class EquipmentCategory
    {
        public EquipmentCategory()
        {
            EqupmentNames = new HashSet<EqupmentName>();
            MonthlyReturns = new HashSet<MonthlyReturn>();
            ShipEquipmentInfos = new HashSet<ShipEquipmentInfo>();
        }

        public int EquipmentCategoryId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int? GroupNameId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName BaseSchoolName { get; set; }
        public virtual GroupName GroupName { get; set; }
        public virtual ICollection<EqupmentName> EqupmentNames { get; set; }
        public virtual ICollection<MonthlyReturn> MonthlyReturns { get; set; }
        public virtual ICollection<ShipEquipmentInfo> ShipEquipmentInfos { get; set; }
    }
}
