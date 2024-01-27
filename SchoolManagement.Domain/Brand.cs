using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class Brand:BaseDomainEntity
    {
        public Brand()
        {
            //ShipEquipmentInfos = new HashSet<ShipEquipmentInfo>();
            HalfYearlyReturns = new HashSet<HalfYearlyReturn>();
        }

        public int BrandId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName? BaseSchoolName { get; set; }
        //public virtual ICollection<ShipEquipmentInfo> ShipEquipmentInfos { get; set; }
        public virtual ICollection<HalfYearlyReturn> HalfYearlyReturns { get; set; }
    }
}
